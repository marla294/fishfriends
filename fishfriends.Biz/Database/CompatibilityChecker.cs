﻿using System;
using System.Collections.Generic;
using fishfriends.Biz.Models;

namespace fishfriends.Biz.Database
{
    public static class CompatibilityChecker
    {
        public static List<FishCompatibility> GetAllFishesCompatibility(List<FishDTO> selectedFishes)
        {
            List<FishCompatibility> allFishesCompatibility = CreateFishPairCompatibilityList();

            for (var i = 0; i < allFishesCompatibility.Count; i++) 
            {
                var currentFishCompatibility = allFishesCompatibility[i];

                SetSelectedFishesCompatibility(currentFishCompatibility, selectedFishes);
            }

            return allFishesCompatibility;
        }

        private static void SetSelectedFishesCompatibility(FishCompatibility fishCompatibility, List<FishDTO> selectedFishes)
        {
            var mainFish = fishCompatibility.MainFish;

            for (var j = 0; j < selectedFishes.Count; j++)
            {
                var selectedFish = selectedFishes[j];
                var compatibility = GetCompatibility(mainFish, selectedFish);

                fishCompatibility.SetFishCompatibility(compatibility);
            }
        }

        private static List<FishCompatibility> CreateFishPairCompatibilityList()
        {
            List<FishCompatibility> fishesCompatibility = new List<FishCompatibility>();
            List<FishDTO> allFish = FishLoader.LoadAll();

            for (var j = 0; j < allFish.Count; j++)
            {
                fishesCompatibility.Add(new FishCompatibility(allFish[j]));
            }

            return fishesCompatibility;
        }

        private static Compatibility GetCompatibility(FishDTO mainFish, FishDTO selectedFish)
        {
            var sql = String.Format("select c.compatible " +
                                        "from compatibility c " +
                                        "inner join fish f1 " +
                                        "on c.fishone = f1.id " +
                                        "inner join fish f2 " +
                                        "on c.fishtwo = f2.id " +
                                        "where f1.name = '{0}' " +
                                        "and f2.name = '{1}';",
                                        mainFish.Name, selectedFish.Name);

            var compatible = ConnectionUtils.ExecuteCommand(new PostgreSQLConnection(), sql)[0][0];

            return new Compatibility(selectedFish, compatible);
        }
    }
}
