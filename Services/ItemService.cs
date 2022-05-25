using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using SwapApp.Authorization;
using SwapApp.Entities;
using SwapApp.Exceptions;
using SwapApp.Models;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SwapApp.Services
{
    public interface IItemService
    {
        int AddItem(AddItemDto addItem);
        PagedResult<GetItemDto> GetAllItems(ItemQuery query);
        GetItemDto GetItemById(int id);
        PagedResult<GetItemDto> GetAllUserItems(ItemQuery query, int id);
        bool UpdateItem(UpdateItemDto updateItem, int id);
        bool ExtendValidity(int id);
        bool ChangeVisibility(int id);
        bool DeleteItem(int id);
        Task UploadPhotos(List<IFormFile> files, int id);
        //List<byte[]> Photos(int id);


    }

    public class ItemService : IItemService
    {
        private readonly ItemDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public ItemService(ItemDbContext dbContext, IMapper mapper, ILogger<ItemService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public int AddItem(AddItemDto addItem)
        {

            var item = _mapper.Map<Item>(addItem);
            item.UserId = (int)_userContextService.GetUserId;
            item.CreatedAt = DateTime.Now;
            item.ExpiresAt = item.CreatedAt.AddDays(14);
            _dbContext.Item.Add(item);
            _dbContext.SaveChanges();
            return item.Id;
        }

        public GetItemDto GetItemById(int id)
        {
            var item = _dbContext.Item
                .Include(f => f.ItemPhotos)
                .FirstOrDefault(x => x.Id == id);

            if (item is null)
                throw new NotFoundException($"Item with id: {id} not found");
            var result = _mapper.Map<GetItemDto>(item);
            return result;
        }

        public PagedResult<GetItemDto> GetAllItems(ItemQuery query)
        {
            var baseQuery = _dbContext.Item
                .Where(d => DateTime.Compare(DateTime.Now, d.ExpiresAt) < 0 && d.IsPublic == true)
                .Where(r => r.ReservedBy == null)
                .Where(e => query.Search == null || (e.Name.ToLower().Contains(query.Search.ToLower()) ||
                                                     e.Description.ToLower().Contains(query.Search.ToLower())))
                .Where(c => query.City == null || c.City.ToLower().Contains(query.City.ToLower()));

            if (!string.IsNullOrEmpty(query.SortBy))
            {

                var columnsSelectors = new Dictionary<string, Expression<Func<Item, object>>>
                {
                    {nameof(Item.Name), r => r.Name},
                    {nameof(Item.CreatedAt), r => r.CreatedAt},
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery.Include(f => f.ItemPhotos).Skip(query.PageSize * query.PageNumber - query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = items.Count();

            var itemsDtos = _mapper.Map<List<GetItemDto>>(items);

            var result = new PagedResult<GetItemDto>(itemsDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;

        }

        public PagedResult<GetItemDto> GetAllUserItems(ItemQuery query, int id)
        {
            var baseQuery = _dbContext.Item
                .Where(c => c.UserId == id)
                .Where(d => DateTime.Compare(DateTime.Now, d.ExpiresAt) < 0 && d.IsPublic == true)
                .Where(e => query.Search == null || (e.Name.ToLower().Contains(query.Search.ToLower()) ||
                                                     e.Description.ToLower().Contains(query.Search.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {

                var columnsSelectors = new Dictionary<string, Expression<Func<Item, object>>>
                {
                    {nameof(Item.Name), r => r.Name},
                    {nameof(Item.CreatedAt), r => r.CreatedAt},
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var userItems = baseQuery.Include(f => f.ItemPhotos).Skip(query.PageSize * query.PageNumber - query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = userItems.Count();

            var itemsDtos = _mapper.Map<List<GetItemDto>>(userItems);

            var result = new PagedResult<GetItemDto>(itemsDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }

        public bool UpdateItem (UpdateItemDto updateItem, int id)
        {
            var item = _dbContext.Item.FirstOrDefault(x => x.Id == id);
            if (item is null)
                throw new NotFoundException("Item not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, item, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if(!authorizationResult.Succeeded)
            {
                throw new ForbidException("You can't update this item.");
            }

            item.Name = updateItem.Name;
            item.Description = updateItem.Description;
            item.District = updateItem.District;
            item.Street = updateItem.Street;
            item.City = updateItem.City;
            item.SwapFor = updateItem.SwapFor;
            item.ForFree = updateItem.ForFree;

            _dbContext.SaveChanges();
            return true;
        }

        public bool ExtendValidity(int id)
        {
            var item = _dbContext.Item.FirstOrDefault(x => x.Id == id);
            if (item is null)
                throw new NotFoundException("Item not found");
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, item, new ResourceOperationRequirement(ResourceOperation.ExtendValidity)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("You can't extend validity of this item.");
            }

            if (DateTime.Compare(DateTime.Now, item.ExpiresAt) < 0)
            {
                throw new ForbidException($"This item is currently valid. It expires at {item.ExpiresAt}");
            }

            item.CreatedAt = DateTime.Now;
            item.ExpiresAt = DateTime.Now.AddDays(14);
            _dbContext.SaveChanges();
            return true;

        }

        public bool ChangeVisibility(int id)
        {
            var item = _dbContext.Item.Where(p => p.ReservedBy == null).FirstOrDefault(x => x.Id == id);
            if (item is null)
                throw new NotFoundException("Item not found or item is already reserved");
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, item, new ResourceOperationRequirement(ResourceOperation.ChangeVisibility)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("You can't change visibility of this item.");
            }

            if (item.IsPublic)
            {
                item.IsPublic = false;
            }
            else
            {
                item.IsPublic = true;
            }

            _dbContext.SaveChanges();

            return true;
        }

        public bool DeleteItem (int id)
        {
            _logger.LogError($"Item with id: {id} DELETE action invoked");
            var item = _dbContext.Item.FirstOrDefault(x => x.Id == id);
            if (item is null) return false;

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, item, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("You can't delete this item");
            }

            _dbContext.Item.Remove(item);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task UploadPhotos(List<IFormFile> files, int id)
        {
            if (files.Count > 6)
                throw new BadRequestException("You cannot add more than 6 photos");
            var result = new List <ItemPhoto>();
            foreach (var file in files)
            {
                var extension = file.ContentType;

                if (file == null)
                    throw new BadRequestException("One or more files are null");
                if (file.Length == 0)
                    throw new BadRequestException("One or more files are zero-length");
                if (!extension.StartsWith("image"))
                    throw new BadRequestException("One or more files are not images");

                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fileExtension = fileName.Split('.')[1];
                fileName = DateTime.Now.Ticks + "." + fileExtension;

                var fullPath = @$"{rootPath}\ItemPhotos\{fileName}";
                await using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                     await file.CopyToAsync(stream);
                }

                result.Add(new ItemPhoto()
                {
                    FilePath = fullPath,
                    ItemId = id
                });
            }
            await _dbContext.ItemPhotos.AddRangeAsync(result);
            await _dbContext.SaveChangesAsync();
        }

        //public List<byte[]> Photos(int id)
        //{
        //    var item = _dbContext.Item
        //        .Include(f => f.ItemPhotos)
        //        .FirstOrDefault(x => x.Id == id);
        //    var photos = item.ItemPhotos;
        //    List<byte[]> b = new List<byte[]>();
        //    foreach (var photo in photos)
        //    {
        //        b.Add(System.IO.File.ReadAllBytes(@$"{photo.FilePath}"));
        //    }

        //    return b;



        //}


    }
}
