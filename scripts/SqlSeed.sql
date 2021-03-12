USE [FoodApp]
GO

--Insert into Ingredients table
INSERT [product].[Ingredients] ([Name], [Price]) VALUES (N'Tomato sauce', CAST(0.00 AS Decimal(18, 2)))
GO
INSERT [product].[Ingredients] ([Name], [Price]) VALUES (N'Mozzarella cheese', CAST(0.00 AS Decimal(18, 2)))
GO
INSERT [product].[Ingredients] ([Name], [Price]) VALUES (N'Pepperoni', CAST(0.00 AS Decimal(18, 2)))
GO

--Insert into ProductDetails table
INSERT [product].[ProductDetails] ([Name], [Description], [Comment]) VALUES (N'Pepperoni', N'Tomato sauce, mozzarella cheese, pepperoni', NULL)
GO

--Insert into ProductIngredients table
INSERT [product].[ProductIngredients] ([ProductDetailId], [IngredientId]) VALUES (1, 1)
GO
INSERT [product].[ProductIngredients] ([ProductDetailId], [IngredientId]) VALUES (1, 2)
GO
INSERT [product].[ProductIngredients] ([ProductDetailId], [IngredientId]) VALUES (1, 3)
GO

--Insert into Products table
INSERT [product].[Products] ([ProductDetailId], [Photo], [Price], [Energy], [Protein], [Fat], [Carbohydrate], [Weight], [Comment], [Diameter], [Kind], [IsArchived]) VALUES (1, NULL, CAST(14.90 AS Decimal(18, 2)), 1370, 14, 13, 40, 360, NULL, 25, 1, 0)
GO
INSERT [product].[Products] ([ProductDetailId], [Photo], [Price], [Energy], [Protein], [Fat], [Carbohydrate], [Weight], [Comment], [Diameter], [Kind], [IsArchived]) VALUES (1, NULL, CAST(23.10 AS Decimal(18, 2)), 1380, 15, 14, 37, 600, NULL, 30, 1, 0)
GO
INSERT [product].[Products] ([ProductDetailId], [Photo], [Price], [Energy], [Protein], [Fat], [Carbohydrate], [Weight], [Comment], [Diameter], [Kind], [IsArchived]) VALUES (1, NULL, CAST(27.40 AS Decimal(18, 2)), 1260, 13, 12, 35, 850, NULL, 35, 1, 0)
GO
INSERT [product].[Products] ([ProductDetailId], [Photo], [Price], [Energy], [Protein], [Fat], [Carbohydrate], [Weight], [Comment], [Diameter], [Kind], [IsArchived]) VALUES (1, NULL, CAST(23.10 AS Decimal(18, 2)), 1510, 16, 17, 36, 400, NULL, 30, 2, 0)
GO
INSERT [product].[Products] ([ProductDetailId], [Photo], [Price], [Energy], [Protein], [Fat], [Carbohydrate], [Weight], [Comment], [Diameter], [Kind], [IsArchived]) VALUES (1, NULL, CAST(27.40 AS Decimal(18, 2)), 1390, 14, 15, 36, 600, NULL, 35, 2, 0)
GO

--Insert into PromoCode table
INSERT [ad].[PromoCodes] ([Code], [Value], [Comment]) VALUES (N'PROMO123', CAST(20.0000 AS Decimal(18, 4)), NULL)
GO
