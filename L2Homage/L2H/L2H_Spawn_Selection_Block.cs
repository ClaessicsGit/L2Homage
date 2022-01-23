using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace L2Homage
{
    public class L2H_Spawn_Selection_Block
    {
        public string ZoneID { get; set; }
        public Border border { get; set; }
        public Button button { get; set; }
        public Image image { get; set; }
        public Image opacityImage { get; set; }
        public List<L2H_Spawn_Selection_Block_Layer> Layers
        {
            get
            {
                List<BitmapImage> images = L2H_Parser.GetWorldZoneLayers(ZoneID);
                List<L2H_Spawn_Selection_Block_Layer> layers = new List<L2H_Spawn_Selection_Block_Layer>();
                for (int i = 0; i < images.Count; i++)
                {
                    layers.Add(new L2H_Spawn_Selection_Block_Layer() { L2H_Spawn_Selection_Block = this, Image = images[i] });
                }

                return layers;
            }

        }

        private int _xCoordinate = 0;
        private int _yCoordinate = 0;
        public int xCoordinate
        {
            get
            {
                if (_xCoordinate == 0)
                {
                    GetCoordinates();
                    return _xCoordinate;
                }
                else
                    return _xCoordinate;
            }
        }
        public int yCoordinate
        {
            get
            {
                if (_yCoordinate == 0)
                {
                    GetCoordinates();
                    return _yCoordinate;
                }
                else
                    return _yCoordinate;
            }
        }


        private void GetCoordinates()
        {
            string[] splitID = ZoneID.Split('_');

            int.TryParse(splitID[0], out _xCoordinate);
            int.TryParse(splitID[1], out _yCoordinate);
        }


        public void SetOpacity(double value)
        {
            opacityImage.Opacity = value;
        }
    }
    public class L2H_Spawn_Selection_Block_Layer
    {
        public L2H_Spawn_Selection_Block L2H_Spawn_Selection_Block { get; set; }
        public BitmapImage Image { get; set; }
        public bool IsSelected { get; set; }
    }
}
