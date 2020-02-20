using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Soeleman
{
    public sealed class GenericRepo :
        IDataRepository
    {
        private static readonly List<ItemGrid> ItemsGrids = new List<ItemGrid>();

        public DataResult GetData(
            int pageSize, 
            int currentPage)
        {
            InitItemGrid();

            return new GenericResult<ItemGrid>
            {
                Records = ItemsGrids
                    .Skip(currentPage)
                    .Take(pageSize)
                    .ToList(),
                TotalRecord = ItemsGrids.Count
            };
        }

        public void AddNewData()
        {
            var rnd = new Random(100000);
            var dat = DateTime.Now;
            var itemRandom = rnd.Next();
            var mod = itemRandom % 2;
            var dec = itemRandom * 0.2M;

            dat = dat.AddDays(0 - (itemRandom / (int)dec));

            ItemsGrids.Add(
                new ItemGrid
                {
                    Id = Guid.NewGuid(),
                    ItemBoolean = mod == 0,
                    ItemDateTime = dat,
                    ItemDecimal = dec,
                    ItemInt32 = rnd.Next(),
                    ItemString = itemRandom.ToString(CultureInfo.InvariantCulture)
                });
        }

        private static void InitItemGrid()
        {
            if (!ItemsGrids.Count.Equals(0))
            {
                return;
            }

            ItemsGrids.Clear();
            ItemsGrids.AddRange(GenerateItemGrid().ToList());
        }

        private static IEnumerable<ItemGrid> GenerateItemGrid()
        {
            var rnd = new Random(100000);
            var dat = DateTime.Now;

            for (var i = 0; i < 1000; i++)
            {
                var itemRandom = rnd.Next();

                var mod = itemRandom % 2;
                var dec = itemRandom * 0.2M;
                dat = dat.AddDays(0 - (itemRandom / (int)dec));

                yield return
                    new ItemGrid
                    {
                        Id = Guid.NewGuid(),
                        ItemBoolean = mod == 0,
                        ItemDateTime = dat,
                        ItemDecimal = dec,
                        ItemInt32 = i,
                        ItemString = itemRandom.ToString(CultureInfo.InvariantCulture)
                    };
            }
        }
    }
}