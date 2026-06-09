using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ForestEscape
{
    public partial class MainWindow : Window
    {
        bool hasRope = false;
        bool hasFlashlight = false;
        bool hasKey = false;
        bool hasFuel = false;
        bool hasAxe = false;
        bool generatorFixed = false;

        int flashlightBattery = 100;
        bool isGameOver = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            IntroScreen.Visibility = Visibility.Collapsed;

            LoadScene("forest");
            UpdateInventory();

            GameText.Text = "Probudil jsem se v lese...";
        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void LoadScene(string scene)
        {
   
            if (isGameOver) return;

            if (hasFlashlight)
            {
                flashlightBattery -= 25; 

                if (flashlightBattery <= 0)
                {
                    flashlightBattery = 0;
                    isGameOver = true;
                    HideAll();
                    UpdateInventory();
                    GameText.Text = "Baterka se úplně vybila! Zůstal jsi potmě v lese a prohrál jsi.";
                    MessageBox.Show("Baterka se vybila! Prohrál jsi. Spusť hru znovu.", "Konec hry");
                    return;
                }
            }

            HideAll();

            switch (scene)
            {
                case "forest":

                    BackgroundImage.Source =
                        new BitmapImage(
                            new System.Uri("Images/forest.png", System.UriKind.Relative));

                    CarSpot.Visibility = Visibility.Visible;
                    BagSpot.Visibility = Visibility.Visible;
                    FlashlightSpot.Visibility = Visibility.Visible;
                    SignSpot.Visibility = Visibility.Visible;
                    CaveSpot.Visibility = Visibility.Visible;

                    break;

                case "cave":

                    BackgroundImage.Source =
                        new BitmapImage(
                            new System.Uri("Images/cave.png", System.UriKind.Relative));

                    BoxSpot.Visibility = Visibility.Visible;
                    KeySpot.Visibility = Visibility.Visible;
                    LanternSpot.Visibility = Visibility.Visible;
                    TunnelSpot.Visibility = Visibility.Visible;
                    BackForestSpot.Visibility = Visibility.Visible;

                    break;

                case "cabin":

                    BackgroundImage.Source =
                        new BitmapImage(
                            new System.Uri("Images/cabin.png", System.UriKind.Relative));

                    CabinetSpot.Visibility = Visibility.Visible;
                    AxeSpot.Visibility = Visibility.Visible;
                    FuelSpot.Visibility = Visibility.Visible;
                    GeneratorSpot.Visibility = Visibility.Visible;
                    BackForestSpot.Visibility = Visibility.Visible;

                    break;

                case "gate":

                    BackgroundImage.Source =
                        new BitmapImage(
                            new System.Uri("Images/gate.png", System.UriKind.Relative));

                    GateSpot.Visibility = Visibility.Visible;
                    BackForestSpot.Visibility = Visibility.Visible;

                    break;
            }
        }

        void HideAll()
        {
            CarSpot.Visibility = Visibility.Hidden;
            BagSpot.Visibility = Visibility.Hidden;
            FlashlightSpot.Visibility = Visibility.Hidden;
            SignSpot.Visibility = Visibility.Hidden;
            CaveSpot.Visibility = Visibility.Hidden;

            BoxSpot.Visibility = Visibility.Hidden;
            KeySpot.Visibility = Visibility.Hidden;
            LanternSpot.Visibility = Visibility.Hidden;
            TunnelSpot.Visibility = Visibility.Hidden;

            CabinetSpot.Visibility = Visibility.Hidden;
            AxeSpot.Visibility = Visibility.Hidden;
            FuelSpot.Visibility = Visibility.Hidden;
            GeneratorSpot.Visibility = Visibility.Hidden;

            GateSpot.Visibility = Visibility.Hidden;

            BackForestSpot.Visibility = Visibility.Hidden;
        }

        void UpdateInventory()
        {
            StringBuilder sb = new StringBuilder();

            if (hasRope) sb.Append("Provaz  ");

            if (hasFlashlight) sb.Append($"Baterka ({flashlightBattery}%)  ");

            if (hasKey) sb.Append("Klíč  ");
            if (hasFuel) sb.Append("Benzín  ");
            if (hasAxe) sb.Append("Sekera  ");

            if (sb.Length == 0)
                sb.Append("Nic");

            InventoryText.Text = sb.ToString();
        }

        private void CarSpot_Click(object sender, RoutedEventArgs e)
        {
            GameText.Text = "Lamborgini je úplně zničené.";
        }

        private void BagSpot_Click(object sender, RoutedEventArgs e)
        {
            if (!hasRope)
            {
                hasRope = true;
                GameText.Text = "Našel jsem provaz.";
                UpdateInventory();
            }
            else
            {
                GameText.Text = "Batoh je prázdný.";
            }
        }

        private void FlashlightSpot_Click(object sender, RoutedEventArgs e)
        {
            if (!hasFlashlight)
            {
                hasFlashlight = true;
                GameText.Text = "Ufff baterka ještě funguje.";
                UpdateInventory();
            }
        }

        private void SignSpot_Click(object sender, RoutedEventArgs e)
        {
            GameText.Text = "Cedule ukazuje směr k myslivecké chatě.";
        }

        private void CaveSpot_Click(object sender, RoutedEventArgs e)
        {
            if (!hasFlashlight)
            {
                GameText.Text = "V jeskyni je moc velká tma.";
            }
            else
            {
                LoadScene("cave");
                GameText.Text = "Vstoupil jsem do jeskyně.";
            }
        }

        private void BoxSpot_Click(object sender, RoutedEventArgs e)
        {
            GameText.Text = "Bedna je prázdná.";
        }

        private void KeySpot_Click(object sender, RoutedEventArgs e)
        {
            if (!hasKey)
            {
                hasKey = true;
                GameText.Text = "Našel jsem starý klíč.";
                UpdateInventory();
            }
        }

        private void LanternSpot_Click(object sender, RoutedEventArgs e)
        {
            GameText.Text = "Světlo furt funguje.";
        }

        private void TunnelSpot_Click(object sender, RoutedEventArgs e)
        {
            LoadScene("cabin");
            GameText.Text = "Tunel vede do staré chaty.";
        }

        private void BackForestSpot_Click(object sender, RoutedEventArgs e)
        {
            LoadScene("forest");
            GameText.Text = "Vrátil jsem se zpátky do lesa.";
        }

        private void CabinetSpot_Click(object sender, RoutedEventArgs e)
        {
            if (hasKey)
            {
                GameText.Text = "Otevřel jsem starou skříň.";
            }
            else
            {
                GameText.Text = "Je zamčený portřebuju klíč..";
            }
        }

        private void AxeSpot_Click(object sender, RoutedEventArgs e)
        {
            if (!hasAxe)
            {
                hasAxe = true;
                GameText.Text = "Vzal jsem sekeru.";
                UpdateInventory();
            }
        }

        private void FuelSpot_Click(object sender, RoutedEventArgs e)
        {
            if (!hasFuel)
            {
                hasFuel = true;
                GameText.Text = "Našel jsem benzín.";
                UpdateInventory();
            }
        }

        private void GeneratorSpot_Click(object sender, RoutedEventArgs e)
        {
            if (hasFuel)
            {
                generatorFixed = true;
                GameText.Text = "Generátor se rozběhl.";
                LoadScene("gate");
            }
            else
            {
                GameText.Text = "Generátor potřebuje benzín.";
            }
        }

        private void GateSpot_Click(object sender, RoutedEventArgs e)
        {
            if (!generatorFixed)
            {
                GameText.Text = "Nejdřív musíš zprovoznit generátor.";
                return;
            }

            if (!hasAxe)
            {
                GameText.Text = "Brána je zarostlá a nejde otevřít. Potřebuješ sekeru.";
                return;
            }

            MessageBox.Show("Zdrhli jsmeeeeeeee!");
            Close();
        }
    }
}