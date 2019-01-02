using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using OilTeamProject.Models.Products;
using OilTeamProject.Dtos;
using OilTeamProject.Persistence;

namespace OilTeamProject.Controllers.Api
{
    public class MessagesController : ApiController
    {
        private ApplicationDbContext _context;
        public MessagesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/messages
        public IHttpActionResult GetMessages()
        {
            var messageDtos = _context.Messages
                .ToList()
                .Select(Mapper.Map<Message, MessageDto>);
            return Ok(messageDtos);
        }
        // GET api/messages/1
        public IHttpActionResult GetMessage(int id)
        {
            var message = _context.Messages.SingleOrDefault(m => m.Id == id);

            if (message == null)
                return NotFound();

            return Ok(Mapper.Map<Message, MessageDto>(message));
        }

        // POST api/messages
        [HttpPost]
        public IHttpActionResult CreateMessage(MessageDto messageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var message = Mapper.Map<MessageDto, Message>(messageDto);
            message.Date = DateTime.Now;
            _context.Messages.Add(message);
            _context.SaveChanges();

            messageDto.Id = message.Id;

            return Created(new Uri(Request.RequestUri + "/" + message.Id), messageDto);
        }

        [HttpPut]
        public void UpdateMessage(int id, MessageDto messageDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var messageInDb = _context.Messages.SingleOrDefault(c => c.Id == id);

            if (messageInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(messageDto, messageInDb);

            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteMessage(int id)
        {
            var messageInDb = _context.Messages.SingleOrDefault(m => m.Id == id);

            if (messageInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Messages.Remove(messageInDb);
            _context.SaveChanges();
        }
    }
}
