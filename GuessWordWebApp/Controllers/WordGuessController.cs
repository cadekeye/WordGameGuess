using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GuessWordWebApp.Controllers
{
    public class WordGuessController : Controller
    {
        private readonly IWordDataService _wordDataService;
        public WordGuessController(IWordDataService wordDataService)
        {
            _wordDataService = wordDataService;
        }

        [HttpGet("AllWords")]
        public async Task<IActionResult> Index()
        {
            var allWords = await _wordDataService.GetAllWordsAsync();

            return View(allWords);
        }
    }
}