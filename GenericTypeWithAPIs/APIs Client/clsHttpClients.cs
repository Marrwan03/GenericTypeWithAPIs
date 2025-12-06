using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclesApp.GlobalClasses
{
    public class clsHttpClients
    {
        /// <summary>
        /// Defining specific client with base address
        /// </summary>
        /// <param name="client">Choose specific from enum</param>
        public static void Defining(clsEnum.enClients client)
        {
            switch (client)
            {
                case clsEnum.enClients.Make:
                    {
                        if (!IsDefined(clsEnum.enClients.Make))
                        {
                            Make = new HttpClient();
                            Make.BaseAddress = GetUri(clsEnum.enClients.Make);
                        } 
                        break;
                    }
                case clsEnum.enClients.Model:
                    {
                        if (!IsDefined(clsEnum.enClients.Model))
                        {
                            Model = new HttpClient();
                            Model.BaseAddress = GetUri(clsEnum.enClients.Model);
                        }   
                        break;
                    }
                case clsEnum.enClients.SubModel:
                    {
                        if (!IsDefined(clsEnum.enClients.SubModel))
                        {
                            SubModel = new HttpClient();
                            SubModel.BaseAddress = GetUri(clsEnum.enClients.SubModel);
                        }
                        break;
                    }
                case clsEnum.enClients.Body:
                    {
                        if (!IsDefined(clsEnum.enClients.Body))
                        {
                            Body = new HttpClient();
                            Body.BaseAddress = GetUri(clsEnum.enClients.Body);
                        }  
                        break;
                    }
                case clsEnum.enClients.DriveType:
                    {
                        if (!IsDefined(clsEnum.enClients.DriveType))
                        {
                            DriveType = new HttpClient();
                            DriveType.BaseAddress = GetUri(clsEnum.enClients.DriveType);
                        }
                        break;
                    }
                case clsEnum.enClients.FuelType:
                    {
                        if (!IsDefined(clsEnum.enClients.FuelType))
                        {
                            FuelType = new HttpClient();
                            FuelType.BaseAddress = GetUri(clsEnum.enClients.FuelType);
                        }  
                        break;
                    }
                case clsEnum.enClients.VehicleDetail:
                    {
                        if (!IsDefined(clsEnum.enClients.VehicleDetail))
                        {
                            VehicleDetail = new HttpClient();
                            VehicleDetail.BaseAddress = GetUri(clsEnum.enClients.VehicleDetail);
                        } 
                        break;
                    }
            }
        }

        /// <summary>
        /// Check if the specific client is defined or not
        /// </summary>
        /// <param name="client">Choose specific from enum</param>
        /// <returns>Result of checking</returns>
        public static bool IsDefined(clsEnum.enClients client)
            => GetBaseAddress(client) != null;
        private static Uri GetBaseAddress(clsEnum.enClients client)
        {
            Uri BaseAddress = null;
            switch (client)
            {
                case clsEnum.enClients.Make:
                    {
                        if(Make != null)
                            BaseAddress = Make.BaseAddress;
                        break;
                    }
                case clsEnum.enClients.Model:
                    {
                        if (Model != null)
                            BaseAddress = Model.BaseAddress;
                        break;
                    }
                case clsEnum.enClients.SubModel:
                    {
                        if (SubModel != null)
                            BaseAddress = SubModel.BaseAddress;
                        break;
                    }
                case clsEnum.enClients.Body:
                    {
                        if (Body != null)
                            BaseAddress = Body.BaseAddress;
                        break;
                    }
                case clsEnum.enClients.DriveType:
                    {
                        if (DriveType != null)
                            BaseAddress = DriveType.BaseAddress;
                        break;
                    }
                case clsEnum.enClients.FuelType:
                    {
                        if (FuelType != null)
                            BaseAddress = FuelType.BaseAddress;
                        break;
                    }
                case clsEnum.enClients.VehicleDetail:
                    {
                        if (VehicleDetail != null)
                            BaseAddress = VehicleDetail.BaseAddress;
                        break;
                    }
            }
            return BaseAddress;

        }
        public static Uri GetUri(clsEnum.enClients client)
        {
            string uriString = null;
            switch (client)
            {
                case clsEnum.enClients.Make:
                    {
                        uriString = "https://localhost:7090/api/Makes/";
                        break;
                    }
                case clsEnum.enClients.Model:
                    {
                        uriString = "https://localhost:7090/api/MakeModels/";
                        break;
                    }
                case clsEnum.enClients.SubModel:
                    {
                        uriString = "https://localhost:7090/api/SubModels/";
                        break;
                    }
                case clsEnum.enClients.Body:
                    {
                        //uriString = "https://localhost:7090/api/Bodies/";
                        uriString = "https://localhost:7090/api/BodiesTest/";
                        break;
                    }
                case clsEnum.enClients.DriveType:
                    {
                        uriString = "https://localhost:7090/api/DriveTypes/";
                        break;
                    }
                case clsEnum.enClients.FuelType:
                    {
                        uriString = "https://localhost:7090/api/FuelTypes/";
                        break;
                    }
                case clsEnum.enClients.VehicleDetail:
                    {
                        uriString = "https://localhost:7090/api/VehicleDetails/";
                        break;
                    }
            }
            if (string.IsNullOrEmpty(uriString))
                return null;
            return new Uri(uriString);
        }

        public static HttpClient Body;
        public static HttpClient Make;
        public static HttpClient Model;
        public static HttpClient SubModel;
        public static HttpClient DriveType;
        public static HttpClient FuelType;
        public static HttpClient VehicleDetail;
    }
}
