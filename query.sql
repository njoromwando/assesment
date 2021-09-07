select CarMaker,CarModel,sum(SalePriceInDollar) as TotalSalePrice
from CarSales
where cast(SaleDate as date)<=cast(getdate() as date)
group by CarMaker,CarModel