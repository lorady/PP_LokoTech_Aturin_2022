using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AturinKozlovNegmatov_Praktika2022_LokoTeh
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.PraktikaEntities Context
        { get; } = new Entities.PraktikaEntities();
    }
}
