﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace StudentsCardsLibraryWPF.Model
{
    //public class UserAppConfigManager                     //Старый класс, ранее отвечавший за создание и редактирование JSON файла, в котором
                                                            //Сохранялись данные по ID пользователя и выбранному методу фильтрации. 
    //{
    //    string UserAppConfigPath;

    //    public UserAppConfigManager(string ConfigPath)
    //    {
    //        UserAppConfigPath = ConfigPath;
    //    }

    //    public UserAppConfig InitializateUserAppConfig()
    //    {
    //        if (File.Exists(UserAppConfigPath))
    //        {
    //            using (var stream = File.OpenRead(UserAppConfigPath))
    //            {
    //                var serializer = new DataContractJsonSerializer(typeof(UserAppConfig));
    //                return (UserAppConfig)serializer.ReadObject(stream);
    //            }
    //        }
    //        else
    //        {
    //            return new UserAppConfig();
    //        }
    //    }



    //    public void UpdateAppConfig(UserAppConfig User)
    //    {
    //        using (var stream = File.Create(UserAppConfigPath))
    //        {
    //            var serializer = new DataContractJsonSerializer(typeof(UserAppConfig));
    //            serializer.WriteObject(stream, User);
    //        }
    //    }
    //}
}
