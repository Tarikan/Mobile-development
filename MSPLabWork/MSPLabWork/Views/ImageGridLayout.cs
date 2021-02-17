using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MSPLabWork.Views
{
    class ImageGridLayout : AbsoluteLayout
    {
        private int _photoCounter = 0;

        private int _gridCounter = 0;

        private double _unit;

        public ImageGridLayout()
        {}

        public void AddElement(ImageSource source)
        {
            /*
            var ret = new BoxView()
            {
                Color = Color.Blue,
                Margin = 5
            };
            */

            var ret = new Image()
            {
                Source = source,
                Margin = 5,
                BackgroundColor = Color.Gray
            };

            switch (_photoCounter % 6)
            {
                case 0:
                    SetLayoutBounds(ret, new Rectangle(0,
                        _gridCounter * 4 * _unit,
                        3 * _unit,
                        3 * _unit));
                    break;
                case 1:
                    SetLayoutBounds(ret, new Rectangle(3 * _unit,
                        _gridCounter * 4 * _unit,
                        2 * _unit,
                        2 * _unit));
                    break;
                case 2:
                    SetLayoutBounds(ret, new Rectangle(3 * _unit,
                        2 * _unit + _gridCounter * 4 * _unit,
                        2 * _unit,
                        2 * _unit));
                    break;
                case 3:
                    SetLayoutBounds(ret, new Rectangle(0,
                        3 * _unit + _gridCounter * 4 * _unit,
                        _unit,
                        _unit));
                    break;
                case 4:
                    SetLayoutBounds(ret, new Rectangle(_unit,
                        3 * _unit + _gridCounter * 4 * _unit,
                        _unit,
                        _unit));
                    break;
                case 5:
                    SetLayoutBounds(ret, new Rectangle(2 * _unit,
                        3 * _unit + _gridCounter * 4 * _unit,
                        _unit,
                        _unit));
                    _gridCounter++;
                    break;
            }
           
            _photoCounter++;

            Children.Add(ret);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            
            //Console.WriteLine(_unit);
            if (width % 5 != _unit)
            {
                _unit = Width / 5;
                Console.WriteLine(_unit);
                var localGridCounter = 0;
                for (int i = 0; i < Children.Count; i++)
                {
                    var ret = Children[i];
                    switch (i % 6)
                    {
                        case 0:
                            SetLayoutBounds(ret, new Rectangle(0,
                                localGridCounter * 4 * _unit,
                                3 * _unit,
                                3 * _unit));
                            break;
                        case 1:
                            SetLayoutBounds(ret, new Rectangle(3 * _unit,
                                localGridCounter * 4 * _unit,
                                2 * _unit,
                                2 * _unit));
                            break;
                        case 2:
                            SetLayoutBounds(ret, new Rectangle(3 * _unit,
                                2 * _unit + localGridCounter * 4 * _unit,
                                2 * _unit,
                                2 * _unit));
                            break;
                        case 3:
                            SetLayoutBounds(ret, new Rectangle(0,
                                3 * _unit + localGridCounter * 4 * _unit,
                                _unit,
                                _unit));
                            break;
                        case 4:
                            SetLayoutBounds(ret, new Rectangle(_unit,
                                3 * _unit + localGridCounter * 4 * _unit,
                                _unit,
                                _unit));
                            break;
                        case 5:
                            SetLayoutBounds(ret, new Rectangle(2 * _unit,
                                3 * _unit + localGridCounter * 4 * _unit,
                                _unit,
                                _unit));
                            localGridCounter++;
                            break;
                    }
                }
                return;
            }
        }
    }
}
