﻿using System.Collections.Generic;
using System.Web.Http;
using fishfriends.Biz.Database;
using fishfriends.Biz.Models;

namespace fishfriends.Service.Controllers
{
    public class FishController : ApiController
    {
        // Get All
        public List<Fish> Get()
        {
            return new FishLoader().FishList;
        }
    }
}