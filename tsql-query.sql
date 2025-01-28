use ECommerceDatabase
go

select * from dbo.products as p
inner join dbo.productsstock as ps
         on ps.productid = p.id
where count > 0 and count < 5 and 
      color = 1 and
      ((price - (price  * 0.03)) between 1000000 and 3000000)
order by price,count
go