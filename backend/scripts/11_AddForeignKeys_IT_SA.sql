-- ================================================
-- Script: 11_AddForeignKeys_IT_SA.sql
-- Description: Add foreign keys for IT Items and Sales tables
-- Date: 2026-01-07
-- ================================================

USE [UniManage]
GO

PRINT '================================================'
PRINT 'Adding IT Items and Sales foreign keys...'
PRINT '================================================'
GO

-- =============================================
-- IT Items Tables
-- =============================================

-- it_items relationships
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_it_items_brand')
BEGIN
    ALTER TABLE [dbo].[it_items]
    ADD CONSTRAINT [FK_it_items_brand]
    FOREIGN KEY ([BrandCode]) REFERENCES [dbo].[it_item_brand]([Code])
    PRINT '✓ it_items.BrandCode -> it_item_brand.Code'
END
ELSE PRINT '- it_items.BrandCode FK exists'
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_it_items_category')
BEGIN
    ALTER TABLE [dbo].[it_items]
    ADD CONSTRAINT [FK_it_items_category]
    FOREIGN KEY ([CategoryCode]) REFERENCES [dbo].[it_item_category]([Code])
    PRINT '✓ it_items.CategoryCode -> it_item_category.Code'
END
ELSE PRINT '- it_items.CategoryCode FK exists'
GO

-- it_item_details -> it_items
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_it_item_details_items')
BEGIN
    ALTER TABLE [dbo].[it_item_details]
    ADD CONSTRAINT [FK_it_item_details_items]
    FOREIGN KEY ([ItemId]) REFERENCES [dbo].[it_items]([Id])
    ON DELETE CASCADE
    PRINT '✓ it_item_details.ItemId -> it_items.Id'
END
ELSE PRINT '- it_item_details.ItemId FK exists'
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_it_item_details_size')
BEGIN
    ALTER TABLE [dbo].[it_item_details]
    ADD CONSTRAINT [FK_it_item_details_size]
    FOREIGN KEY ([SizeCode]) REFERENCES [dbo].[it_item_size]([Code])
    PRINT '✓ it_item_details.SizeCode -> it_item_size.Code'
END
ELSE PRINT '- it_item_details.SizeCode FK exists'
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_it_item_details_color')
BEGIN
    ALTER TABLE [dbo].[it_item_details]
    ADD CONSTRAINT [FK_it_item_details_color]
    FOREIGN KEY ([ColorCode]) REFERENCES [dbo].[it_item_color]([Code])
    PRINT '✓ it_item_details.ColorCode -> it_item_color.Code'
END
ELSE PRINT '- it_item_details.ColorCode FK exists'
GO

-- it_item_image -> it_items
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_it_item_image_items')
BEGIN
    ALTER TABLE [dbo].[it_item_image]
    ADD CONSTRAINT [FK_it_item_image_items]
    FOREIGN KEY ([ItemId]) REFERENCES [dbo].[it_items]([Id])
    ON DELETE CASCADE
    PRINT '✓ it_item_image.ItemId -> it_items.Id'
END
ELSE PRINT '- it_item_image.ItemId FK exists'
GO

-- it_item_inventory -> it_item_details
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_it_item_inventory_details')
BEGIN
    ALTER TABLE [dbo].[it_item_inventory]
    ADD CONSTRAINT [FK_it_item_inventory_details]
    FOREIGN KEY ([ItemDetailId]) REFERENCES [dbo].[it_item_details]([Id])
    PRINT '✓ it_item_inventory.ItemDetailId -> it_item_details.Id'
END
ELSE PRINT '- it_item_inventory.ItemDetailId FK exists'
GO

-- it_item_price -> it_item_details
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_it_item_price_details')
BEGIN
    ALTER TABLE [dbo].[it_item_price]
    ADD CONSTRAINT [FK_it_item_price_details]
    FOREIGN KEY ([ItemDetailId]) REFERENCES [dbo].[it_item_details]([Id])
    PRINT '✓ it_item_price.ItemDetailId -> it_item_details.Id'
END
ELSE PRINT '- it_item_price.ItemDetailId FK exists'
GO

-- it_item_review -> it_items
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_it_item_review_items')
BEGIN
    ALTER TABLE [dbo].[it_item_review]
    ADD CONSTRAINT [FK_it_item_review_items]
    FOREIGN KEY ([ItemId]) REFERENCES [dbo].[it_items]([Id])
    ON DELETE CASCADE
    PRINT '✓ it_item_review.ItemId -> it_items.Id'
END
ELSE PRINT '- it_item_review.ItemId FK exists'
GO

-- it_item_tag_map
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_it_item_tag_map_items')
BEGIN
    ALTER TABLE [dbo].[it_item_tag_map]
    ADD CONSTRAINT [FK_it_item_tag_map_items]
    FOREIGN KEY ([ItemId]) REFERENCES [dbo].[it_items]([Id])
    ON DELETE CASCADE
    PRINT '✓ it_item_tag_map.ItemId -> it_items.Id'
END
ELSE PRINT '- it_item_tag_map.ItemId FK exists'
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_it_item_tag_map_tags')
BEGIN
    ALTER TABLE [dbo].[it_item_tag_map]
    ADD CONSTRAINT [FK_it_item_tag_map_tags]
    FOREIGN KEY ([TagId]) REFERENCES [dbo].[it_item_tag]([Id])
    ON DELETE CASCADE
    PRINT '✓ it_item_tag_map.TagId -> it_item_tag.Id'
END
ELSE PRINT '- it_item_tag_map.TagId FK exists'
GO

-- =============================================
-- Sales Tables
-- =============================================

-- sa_orders -> sa_customers
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_sa_orders_customers')
BEGIN
    ALTER TABLE [dbo].[sa_orders]
    ADD CONSTRAINT [FK_sa_orders_customers]
    FOREIGN KEY ([CustomerCode]) REFERENCES [dbo].[sa_customers]([CustomerCode])
    PRINT '✓ sa_orders.CustomerCode -> sa_customers.CustomerCode'
END
ELSE PRINT '- sa_orders.CustomerCode FK exists'
GO

-- sa_order_items -> sa_orders
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_sa_order_items_orders')
BEGIN
    ALTER TABLE [dbo].[sa_order_items]
    ADD CONSTRAINT [FK_sa_order_items_orders]
    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[sa_orders]([Id])
    ON DELETE CASCADE
    PRINT '✓ sa_order_items.OrderId -> sa_orders.Id'
END
ELSE PRINT '- sa_order_items.OrderId FK exists'
GO

-- sa_order_items -> it_item_details
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_sa_order_items_item_details')
BEGIN
    ALTER TABLE [dbo].[sa_order_items]
    ADD CONSTRAINT [FK_sa_order_items_item_details]
    FOREIGN KEY ([ItemDetailId]) REFERENCES [dbo].[it_item_details]([Id])
    PRINT '✓ sa_order_items.ItemDetailId -> it_item_details.Id'
END
ELSE PRINT '- sa_order_items.ItemDetailId FK exists'
GO

-- sa_payments -> sa_orders
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_sa_payments_orders')
BEGIN
    ALTER TABLE [dbo].[sa_payments]
    ADD CONSTRAINT [FK_sa_payments_orders]
    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[sa_orders]([Id])
    PRINT '✓ sa_payments.OrderId -> sa_orders.Id'
END
ELSE PRINT '- sa_payments.OrderId FK exists'
GO

PRINT '================================================'
PRINT 'IT Items and Sales foreign keys completed!'
PRINT '================================================'
GO
