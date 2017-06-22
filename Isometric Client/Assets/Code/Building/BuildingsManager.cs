﻿using System;
using Assets.Code.Common;
using Isometric.Core;
using UnityEngine;

namespace Assets.Code.Building
{
    public class BuildingsManager : SingletonBehaviour<BuildingsManager>
    {
        public BuildingImage SelectedBuilding { get; set; }

        public BuildingImage[,] Buildings;



        public void SetBuilding(Vector position, string buildingName)
        {
            if (Buildings == null)
            {
                throw new NullReferenceException("Call ShowArea() before SetBuilding()");
            }

            if (Buildings[position.X, position.Y].Building != null)
            {
                Destroy(Buildings[position.X, position.Y].Building);
            }

            (Buildings[position.X, position.Y].Building = Instantiate(GetPrefab(buildingName)))
                .GetComponent<IsometricController>()
                .IsometricPosition = position.ToVector2();
        }

        public void SetUpgrade(Vector position, string buildingName, TimeSpan time)
        {
            var image = Buildings[position.X, position.Y];

            image.Timer = Instantiate(
                Prefabs.Current.BuildingTimer,
                image.Building.transform.position - new Vector3(0, 0.5f, 0),
                new Quaternion())
                .GetComponent<Timer>();

            image.Timer.Value = time;
            SetBuilding(position, buildingName);
        }

        public void ShowArea(string[,] names)
        {
            Buildings = new BuildingImage[names.GetLength(0), names.GetLength(1)];

            for (var x = 0; x < names.GetLength(0); x++)
            {
                for (var y = 0; y < names.GetLength(1); y++)
                {
                    Buildings[x, y] = new BuildingImage
                    {
                        Holder = Instantiate(Prefabs.Current.Holder),
                        Position = new Vector(x, y),
                        Name = names[x, y]
                    };
                    
                    Buildings[x, y].Holder
                        .GetComponent<IsometricController>()
                        .IsometricPosition = new Vector2(x, y);

                    SetBuilding(new Vector(x, y), names[x, y]);
                }
            }
        }



        private static GameObject GetPrefab(string buildingName)
        {
            switch (buildingName)
            {
                case "Plain":
                    return Prefabs.Current.Plain;

                case "Forest":
                    return Prefabs.Current.Forest;

                case "House":
                    return Prefabs.Current.House;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}