﻿using System;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using osu.Game.Graphics;

namespace osu.Game.Screens.Select
{
    public class FilterControl : Container
    {
        public enum SortMode
        {
            Arist,
            BPM,
            Creator,
            DateAdded,
            Difficulty,
            Length,
            RankAchieved,
            Title
        }

        public enum GroupMode
        {
            NoGrouping,
            Arist,
            BPM,
            Creator,
            DateAdded,
            Difficulty,
            Length,
            RankAchieved,
            Title,
            Collections,
            Favorites,
            MyMaps,
            RankedStatus,
            RecentlyPlayed
        }
    
        public Action FilterChanged;

        public string Search { get; private set; } = string.Empty;
        public SortMode Sort { get; private set; } = SortMode.Title;

        public FilterControl()
        {
            AutoSizeAxes = Axes.Y;

            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Black,
                    Alpha = 0.6f,
                    RelativeSizeAxes = Axes.Both,
                },
                new FlowContainer
                {
                    Padding = new MarginPadding(20),
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Width = 0.4f, // TODO: InnerWidth property or something
                    Direction = FlowDirection.VerticalOnly,
                    Children = new Drawable[]
                    {
                        new SearchTextBox { RelativeSizeAxes = Axes.X },
                        new GroupSortTabs()
                    }
                }
            };
        }

        private class TabItem : ClickableContainer
        {
            public string Text
            {
                get { return text.Text; }
                set { text.Text = value; }
            }

            private void FadeActive()
            {
                box.FadeIn(300);
                text.FadeColour(Color4.White, 300);
            }

            private void FadeInactive()
            {
                box.FadeOut(300);
                text.FadeColour(fadeColour, 300);
            }

            private bool active;
            public bool Active
            {
                get { return active; }
                set
                {
                    active = value;
                    if (active)
                        FadeActive();
                    else
                        FadeInactive();
                }
            }
        
            private SpriteText text;
            private Box box;
            private Color4 fadeColour;
            
            protected override bool OnHover(InputState state)
            {
                if (!active)
                    FadeActive();
                return true;
            }
            
            protected override void OnHoverLost(InputState state)
            {
                if (!active)
                    FadeInactive();
            }
        
            public TabItem()
            {
                AutoSizeAxes = Axes.Both;
                Children = new Drawable[]
                {
                    text = new SpriteText
                    {
                        Margin = new MarginPadding(5),
                        TextSize = 14,
                        Font = @"Exo2.0-Bold",
                    },
                    box = new Box
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 1,
                        Alpha = 0,
                        Colour = Color4.White,
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft,
                    }
                };
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                text.Colour = colours.Blue;
                fadeColour = colours.Blue;
            }
        }

        private class GroupSortTabs : Container
        {
            private TextAwesome groupsEllipsis, sortEllipsis;
            private SpriteText sortLabel;
        
            public GroupSortTabs()
            {
                RelativeSizeAxes = Axes.X;
                AutoSizeAxes = Axes.Y;
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 1,
                        Colour = OsuColour.Gray(80),
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft,
                    },
                    new FlowContainer
                    {
                        AutoSizeAxes = Axes.Both,
                        Direction = FlowDirection.HorizontalOnly,
                        Spacing = new Vector2(10, 0),
                        Children = new Drawable[]
                        {
                            new TabItem
                            {
                                Text = "All",
                                Active = true,
                            },
                            new TabItem
                            {
                                Text = "Recently Played",
                                Active = false,
                            },
                            new TabItem
                            {
                                Text = "Collections",
                                Active = false,
                            },
                            groupsEllipsis = new TextAwesome
                            {
                                Icon = FontAwesome.fa_ellipsis_h,
                                TextSize = 14,
                                Margin = new MarginPadding { Top = 5, Bottom = 5 },
                                Origin = Anchor.BottomLeft,
                                Anchor = Anchor.BottomLeft,
                            }
                        }
                    },
                    new FlowContainer
                    {
                        AutoSizeAxes = Axes.Both,
                        Direction = FlowDirection.HorizontalOnly,
                        Spacing = new Vector2(10, 0),
                        Origin = Anchor.TopRight,
                        Anchor = Anchor.TopRight,
                        Children = new Drawable[]
                        {
                            sortLabel = new SpriteText
                            {
                                Font = @"Exo2.0-Bold",
                                Text = "Sort results by",
                                TextSize = 14,
                                Margin = new MarginPadding { Top = 5, Bottom = 5 },
                            },
                            new TabItem
                            {
                                Text = "Artist",
                                Active = true,
                            },
                            sortEllipsis = new TextAwesome
                            {
                                Icon = FontAwesome.fa_ellipsis_h,
                                TextSize = 14,
                                Margin = new MarginPadding { Top = 5, Bottom = 5 },
                                Origin = Anchor.BottomLeft,
                                Anchor = Anchor.BottomLeft,
                            }
                        }
                    },
                };
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                groupsEllipsis.Colour = colours.Blue;
                sortLabel.Colour = colours.GreenLight;
                sortEllipsis.Colour = colours.GreenLight;
            }
        }
    }
}