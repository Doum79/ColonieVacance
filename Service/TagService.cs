using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using Model.Exceptions;
using Provider.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class TagService
    {
        private readonly ILogger<TagService> logger;
        private readonly Context context;
        private readonly IConfiguration config;

        public TagService(ILogger<TagService> logger, Context context, IConfiguration config) { 
            this.logger = logger;
            this.context = context;
            this.config = config;
        }
        
        public async Task<Tag> GetTag(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            return await context.Tag.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Tag>> ListTag()
        {
            return await context.Tag.AsNoTracking().ToListAsync();
        }

        public async Task<Tag> AddTag(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException();

            CheckTag(tag);

            context.Tag.Add(tag);
            await context.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag> UpdateTag(Tag tag)
        {
            if (tag == null && tag.Id == null)
                throw new ArgumentNullException();

            CheckTag(tag);

            var oldTag = await GetTag(tag.Id);

            oldTag.Label = tag.Label;

            await context.SaveChangesAsync();

            return tag;
        }

        public async Task RemoveTag(int? tagId)
        {
            if (tagId == null)
                throw new ArgumentNullException();

            var tag = await GetTag(tagId);

            context.Tag.Remove(tag);
            await context.SaveChangesAsync();
        }

        private void CheckTag(Tag tag)
        {
            if (string.IsNullOrEmpty(tag.Label))
                throw new ThematicLabelException();
        }
    }
}
