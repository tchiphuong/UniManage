USE UniManage;
GO

-- 1. CLEAN UP
DELETE FROM sy_function_mapping;
DBCC CHECKIDENT ('sy_function_mapping', RESEED, 0);

DELETE FROM sy_role_permissions;
DBCC CHECKIDENT ('sy_role_permissions', RESEED, 0);

DELETE FROM sy_menus;
DBCC CHECKIDENT ('sy_menus', RESEED, 0);

DELETE FROM sy_functions;
DBCC CHECKIDENT ('sy_functions', RESEED, 0);

DELETE FROM sy_function_groups;
DBCC CHECKIDENT ('sy_function_groups', RESEED, 0);

DELETE FROM sy_modules;
-- Note: DBCC CHECKIDENT for sy_modules if it has an identity column.
-- Let's try it, if not it will just throw a warning or error. We'll skip it for sy_modules if it fails, but let's just add it.
-- Actually sy_modules doesn't usually have an ID identity if Code is PK. Let's just do it for tables we know have it.

DECLARE @Now DATETIME2 = GETDATE();

-- 2. INSERT MODULES
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('DASHBOARD', 'module.dashboard', 10, 'chart-pie', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MASTER_DATA', 'module.masterData', 20, 'database', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SALES', 'module.sales', 30, 'shopping-cart', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PURCHASING', 'module.purchasing', 40, 'shopping-bag', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('INVENTORY', 'module.inventory', 50, 'archive-box', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PRODUCTION', 'module.production', 60, 'building-office', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('QUALITY_MANAGEMENT', 'module.qualityManagement', 70, 'shield-check', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('FINANCE_ACCOUNTING', 'module.financeAccounting', 80, 'currency-dollar', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('HUMAN_RESOURCES', 'module.humanResources', 90, 'users', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('CRM', 'module.crm', 100, 'hand-raised', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PROJECT_MANAGEMENT', 'module.projectManagement', 110, 'clipboard-document-list', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('ASSET_MANAGEMENT', 'module.assetManagement', 120, 'cube', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('WORKFLOW', 'module.workflow', 130, 'rectangle-group', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('DOCUMENT_MANAGEMENT', 'module.documentManagement', 140, 'folder-open', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('REPORTS', 'module.reports', 150, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_modules (Code, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SYSTEM', 'module.system', 160, 'cog-6-tooth', 1, 'SYSTEM', @Now);

-- 3. INSERT GROUPS
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('DB_OVERVIEW', 'DASHBOARD', 'func.group.db.overview', 10, 'chart-pie', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('DB_MY_WORKSPACE', 'DASHBOARD', 'func.group.db.myWorkspace', 20, 'briefcase', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('DB_QUICK_REPORTS', 'DASHBOARD', 'func.group.db.quickReports', 30, 'bolt', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MS_ORGANIZATION', 'MASTER_DATA', 'func.group.ms.organization', 10, 'building-office-2', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MS_BUSINESS_PARTNERS', 'MASTER_DATA', 'func.group.ms.businessPartners', 20, 'hand-raised', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MS_INVENTORY', 'MASTER_DATA', 'func.group.ms.inventory', 30, 'archive-box', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MS_PURCHASING', 'MASTER_DATA', 'func.group.ms.purchasing', 40, 'shopping-bag', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MS_SALES', 'MASTER_DATA', 'func.group.ms.sales', 50, 'shopping-cart', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MS_PRODUCTION', 'MASTER_DATA', 'func.group.ms.production', 60, 'building-office', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MS_QUALITY', 'MASTER_DATA', 'func.group.ms.quality', 70, 'shield-check', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MS_FINANCE', 'MASTER_DATA', 'func.group.ms.finance', 80, 'squares-2x2', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MS_HUMAN_RESOURCES', 'MASTER_DATA', 'func.group.ms.humanResources', 90, 'users', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MS_ASSETS', 'MASTER_DATA', 'func.group.ms.assets', 100, 'cube', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MS_PROJECTS', 'MASTER_DATA', 'func.group.ms.projects', 110, 'clipboard-document-list', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('MS_COMMON', 'MASTER_DATA', 'func.group.ms.common', 120, 'globe-alt', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SA_QUOTATION', 'SALES', 'func.group.sa.quotation', 10, 'document-duplicate', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SA_SALES_ORDER', 'SALES', 'func.group.sa.salesOrder', 20, 'document-text', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SA_DELIVERY', 'SALES', 'func.group.sa.delivery', 30, 'truck', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SA_RETURN', 'SALES', 'func.group.sa.return', 40, 'arrow-path', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SA_INVOICE', 'SALES', 'func.group.sa.invoice', 50, 'receipt-percent', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SA_REPORTS', 'SALES', 'func.group.sa.reports', 60, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PU_PURCHASE_REQUEST', 'PURCHASING', 'func.group.pu.purchaseRequest', 10, 'inbox-arrow-down', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PU_RFQ', 'PURCHASING', 'func.group.pu.rfq', 20, 'chat-bubble-left-right', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PU_PURCHASE_ORDER', 'PURCHASING', 'func.group.pu.purchaseOrder', 30, 'document-check', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PU_GOODS_RECEIPT', 'PURCHASING', 'func.group.pu.goodsReceipt', 40, 'inbox', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PU_SUPPLIER_RETURN', 'PURCHASING', 'func.group.pu.supplierReturn', 50, 'arrow-uturn-left', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PU_REPORTS', 'PURCHASING', 'func.group.pu.reports', 60, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('IV_RECEIVING', 'INVENTORY', 'func.group.iv.receiving', 10, 'arrow-down-tray', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('IV_ISSUING', 'INVENTORY', 'func.group.iv.issuing', 20, 'arrow-up-tray', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('IV_TRANSFER', 'INVENTORY', 'func.group.iv.transfer', 30, 'arrows-right-left', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('IV_STOCK_COUNT', 'INVENTORY', 'func.group.iv.stockCount', 40, 'clipboard-document-check', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('IV_LOT_SERIAL', 'INVENTORY', 'func.group.iv.lotSerial', 50, 'qr-code', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('IV_REPORTS', 'INVENTORY', 'func.group.iv.reports', 60, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PR_PLANNING', 'PRODUCTION', 'func.group.pr.planning', 10, 'calendar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PR_PRODUCTION_ORDER', 'PRODUCTION', 'func.group.pr.productionOrder', 20, 'document-text', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PR_MATERIAL_ISSUE', 'PRODUCTION', 'func.group.pr.materialIssue', 30, 'truck', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PR_SHOP_FLOOR', 'PRODUCTION', 'func.group.pr.shopFloor', 40, 'building-storefront', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PR_FINISHED_GOODS', 'PRODUCTION', 'func.group.pr.finishedGoods', 50, 'gift', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PR_REPORTS', 'PRODUCTION', 'func.group.pr.reports', 60, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('QM_INCOMING_INSPECTION', 'QUALITY_MANAGEMENT', 'func.group.qm.incomingInspection', 10, 'magnifying-glass-plus', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('QM_IN_PROCESS_INSPECTION', 'QUALITY_MANAGEMENT', 'func.group.qm.inProcessInspection', 20, 'magnifying-glass', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('QM_FINAL_INSPECTION', 'QUALITY_MANAGEMENT', 'func.group.qm.finalInspection', 30, 'check-badge', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('QM_NON_CONFORMANCE', 'QUALITY_MANAGEMENT', 'func.group.qm.nonConformance', 40, 'exclamation-triangle', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('QM_REPORTS', 'QUALITY_MANAGEMENT', 'func.group.qm.reports', 50, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('FI_GENERAL_LEDGER', 'FINANCE_ACCOUNTING', 'func.group.fi.generalLedger', 10, 'book-open', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('FI_ACCOUNTS_RECEIVABLE', 'FINANCE_ACCOUNTING', 'func.group.fi.accountsReceivable', 20, 'arrow-right-circle', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('FI_ACCOUNTS_PAYABLE', 'FINANCE_ACCOUNTING', 'func.group.fi.accountsPayable', 30, 'arrow-left-circle', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('FI_CASH', 'FINANCE_ACCOUNTING', 'func.group.fi.cash', 40, 'banknotes', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('FI_BANK', 'FINANCE_ACCOUNTING', 'func.group.fi.bank', 50, 'building-library', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('FI_FIXED_ASSETS', 'FINANCE_ACCOUNTING', 'func.group.fi.fixedAssets', 60, 'cube-transparent', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('FI_BUDGET', 'FINANCE_ACCOUNTING', 'func.group.fi.budget', 70, 'scale', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('FI_REPORTS', 'FINANCE_ACCOUNTING', 'func.group.fi.reports', 80, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('HR_EMPLOYEE', 'HUMAN_RESOURCES', 'func.group.hr.employee', 10, 'identification', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('HR_RECRUITMENT', 'HUMAN_RESOURCES', 'func.group.hr.recruitment', 20, 'user-plus', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('HR_ATTENDANCE', 'HUMAN_RESOURCES', 'func.group.hr.attendance', 30, 'clock', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('HR_LEAVE', 'HUMAN_RESOURCES', 'func.group.hr.leave', 40, 'calendar-days', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('HR_OVERTIME', 'HUMAN_RESOURCES', 'func.group.hr.overtime', 50, 'moon', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('HR_PAYROLL', 'HUMAN_RESOURCES', 'func.group.hr.payroll', 60, 'currency-dollar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('HR_PERFORMANCE', 'HUMAN_RESOURCES', 'func.group.hr.performance', 70, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('HR_TRAINING', 'HUMAN_RESOURCES', 'func.group.hr.training', 80, 'academic-cap', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('HR_REPORTS', 'HUMAN_RESOURCES', 'func.group.hr.reports', 90, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('CR_LEAD', 'CRM', 'func.group.cr.lead', 10, 'funnel', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('CR_OPPORTUNITY', 'CRM', 'func.group.cr.opportunity', 20, 'light-bulb', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('CR_ACTIVITIES', 'CRM', 'func.group.cr.activities', 30, 'bell', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('CR_CAMPAIGN', 'CRM', 'func.group.cr.campaign', 40, 'megaphone', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('CR_CUSTOMER_SERVICE', 'CRM', 'func.group.cr.customerService', 50, 'lifebuoy', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PM_PROJECT', 'PROJECT_MANAGEMENT', 'func.group.pm.project', 10, 'clipboard-document-list', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PM_PLANNING', 'PROJECT_MANAGEMENT', 'func.group.pm.planning', 20, 'calendar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PM_EXECUTION', 'PROJECT_MANAGEMENT', 'func.group.pm.execution', 30, 'play', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PM_COST', 'PROJECT_MANAGEMENT', 'func.group.pm.cost', 40, 'currency-dollar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('PM_REPORTS', 'PROJECT_MANAGEMENT', 'func.group.pm.reports', 50, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('AM_ASSETS', 'ASSET_MANAGEMENT', 'func.group.am.assets', 10, 'cube', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('AM_ASSIGNMENT', 'ASSET_MANAGEMENT', 'func.group.am.assignment', 20, 'user-group', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('AM_MAINTENANCE', 'ASSET_MANAGEMENT', 'func.group.am.maintenance', 30, 'wrench', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('AM_INVENTORY', 'ASSET_MANAGEMENT', 'func.group.am.inventory', 40, 'archive-box', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('AM_REPORTS', 'ASSET_MANAGEMENT', 'func.group.am.reports', 50, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('WF_APPROVAL', 'WORKFLOW', 'func.group.wf.approval', 10, 'check-circle', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('WF_WORKFLOW', 'WORKFLOW', 'func.group.wf.workflow', 20, 'arrow-path-rounded-square', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('WF_DELEGATION', 'WORKFLOW', 'func.group.wf.delegation', 30, 'user-circle', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('DM_DOCUMENTS', 'DOCUMENT_MANAGEMENT', 'func.group.dm.documents', 10, 'document-duplicate', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('DM_CATEGORIES', 'DOCUMENT_MANAGEMENT', 'func.group.dm.categories', 20, 'folder', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('DM_APPROVAL', 'DOCUMENT_MANAGEMENT', 'func.group.dm.approval', 30, 'check-circle', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('DM_SEARCH', 'DOCUMENT_MANAGEMENT', 'func.group.dm.search', 40, 'magnifying-glass', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('RP_OPERATIONAL_REPORTS', 'REPORTS', 'func.group.rp.operationalReports', 10, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('RP_FINANCIAL_REPORTS', 'REPORTS', 'func.group.rp.financialReports', 20, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('RP_MANAGEMENT_REPORTS', 'REPORTS', 'func.group.rp.managementReports', 30, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('RP_CUSTOM_REPORTS', 'REPORTS', 'func.group.rp.customReports', 40, 'chart-bar', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SY_SECURITY', 'SYSTEM', 'func.group.sy.security', 10, 'shield-check', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SY_NAVIGATION', 'SYSTEM', 'func.group.sy.navigation', 20, 'bars-3', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SY_CONFIGURATION', 'SYSTEM', 'func.group.sy.configuration', 30, 'cog-8-tooth', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SY_NUMBERING', 'SYSTEM', 'func.group.sy.numbering', 40, 'hashtag', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SY_TEMPLATES', 'SYSTEM', 'func.group.sy.templates', 50, 'document-duplicate', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SY_INTEGRATION', 'SYSTEM', 'func.group.sy.integration', 60, 'link', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SY_AUDIT', 'SYSTEM', 'func.group.sy.audit', 70, 'eye', 1, 'SYSTEM', @Now);
INSERT INTO sy_function_groups (Code, ModuleCode, ResourceKey, Sort, Icon, IsActive, CreatedBy, CreatedAt) VALUES ('SY_UTILITIES', 'SYSTEM', 'func.group.sy.utilities', 80, 'wrench-screwdriver', 1, 'SYSTEM', @Now);

-- 4. INSERT FUNCTIONS
INSERT INTO sy_functions (Code, ResourceKey, GroupCode, SortOrder, IsActive, CreatedBy, CreatedAt) VALUES
    ('DB_EXECUTIVE_DASHBOARD', 'func.db.executiveDashboard', 'DB_OVERVIEW', 10, 1, 'SYSTEM', @Now),
    ('DB_SALES_DASHBOARD', 'func.db.salesDashboard', 'DB_OVERVIEW', 20, 1, 'SYSTEM', @Now),
    ('DB_PRODUCTION_DASHBOARD', 'func.db.productionDashboard', 'DB_OVERVIEW', 30, 1, 'SYSTEM', @Now),
    ('DB_FINANCE_DASHBOARD', 'func.db.financeDashboard', 'DB_OVERVIEW', 40, 1, 'SYSTEM', @Now),
    ('DB_MY_TASKS', 'func.db.myTasks', 'DB_MY_WORKSPACE', 10, 1, 'SYSTEM', @Now),
    ('DB_PENDING_APPROVALS', 'func.db.pendingApprovals', 'DB_MY_WORKSPACE', 20, 1, 'SYSTEM', @Now),
    ('DB_NOTIFICATIONS', 'func.db.notifications', 'DB_MY_WORKSPACE', 30, 1, 'SYSTEM', @Now),
    ('DB_SALES_SUMMARY', 'func.db.salesSummary', 'DB_QUICK_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('DB_INVENTORY_SUMMARY', 'func.db.inventorySummary', 'DB_QUICK_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('DB_FINANCIAL_SUMMARY', 'func.db.financialSummary', 'DB_QUICK_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('DB_PRODUCTION_SUMMARY', 'func.db.productionSummary', 'DB_QUICK_REPORTS', 40, 1, 'SYSTEM', @Now),
    ('MS_COMPANY', 'func.ms.company', 'MS_ORGANIZATION', 10, 1, 'SYSTEM', @Now),
    ('MS_BRANCH', 'func.ms.branch', 'MS_ORGANIZATION', 20, 1, 'SYSTEM', @Now),
    ('MS_PLANT', 'func.ms.plant', 'MS_ORGANIZATION', 30, 1, 'SYSTEM', @Now),
    ('MS_BUSINESS_UNIT', 'func.ms.businessUnit', 'MS_ORGANIZATION', 40, 1, 'SYSTEM', @Now),
    ('MS_DEPARTMENT', 'func.ms.department', 'MS_ORGANIZATION', 50, 1, 'SYSTEM', @Now),
    ('MS_SECTION', 'func.ms.section', 'MS_ORGANIZATION', 60, 1, 'SYSTEM', @Now),
    ('MS_TEAM', 'func.ms.team', 'MS_ORGANIZATION', 70, 1, 'SYSTEM', @Now),
    ('MS_COST_CENTER', 'func.ms.costCenter', 'MS_ORGANIZATION', 80, 1, 'SYSTEM', @Now),
    ('MS_PROFIT_CENTER', 'func.ms.profitCenter', 'MS_ORGANIZATION', 90, 1, 'SYSTEM', @Now),
    ('MS_WORK_CENTER', 'func.ms.workCenter', 'MS_ORGANIZATION', 100, 1, 'SYSTEM', @Now),
    ('MS_CUSTOMER', 'func.ms.customer', 'MS_BUSINESS_PARTNERS', 10, 1, 'SYSTEM', @Now),
    ('MS_CUSTOMER_GROUP', 'func.ms.customerGroup', 'MS_BUSINESS_PARTNERS', 20, 1, 'SYSTEM', @Now),
    ('MS_SUPPLIER', 'func.ms.supplier', 'MS_BUSINESS_PARTNERS', 30, 1, 'SYSTEM', @Now),
    ('MS_SUPPLIER_GROUP', 'func.ms.supplierGroup', 'MS_BUSINESS_PARTNERS', 40, 1, 'SYSTEM', @Now),
    ('MS_CONTACT_PERSON', 'func.ms.contactPerson', 'MS_BUSINESS_PARTNERS', 50, 1, 'SYSTEM', @Now),
    ('MS_CARRIER', 'func.ms.carrier', 'MS_BUSINESS_PARTNERS', 60, 1, 'SYSTEM', @Now),
    ('MS_DISTRIBUTOR', 'func.ms.distributor', 'MS_BUSINESS_PARTNERS', 70, 1, 'SYSTEM', @Now),
    ('MS_ITEM', 'func.ms.item', 'MS_INVENTORY', 10, 1, 'SYSTEM', @Now),
    ('MS_ITEM_CATEGORY', 'func.ms.itemCategory', 'MS_INVENTORY', 20, 1, 'SYSTEM', @Now),
    ('MS_ITEM_GROUP', 'func.ms.itemGroup', 'MS_INVENTORY', 30, 1, 'SYSTEM', @Now),
    ('MS_BRAND', 'func.ms.brand', 'MS_INVENTORY', 40, 1, 'SYSTEM', @Now),
    ('MS_MANUFACTURER', 'func.ms.manufacturer', 'MS_INVENTORY', 50, 1, 'SYSTEM', @Now),
    ('MS_UNIT', 'func.ms.unit', 'MS_INVENTORY', 60, 1, 'SYSTEM', @Now),
    ('MS_UNIT_CONVERSION', 'func.ms.unitConversion', 'MS_INVENTORY', 70, 1, 'SYSTEM', @Now),
    ('MS_BARCODE', 'func.ms.barcode', 'MS_INVENTORY', 80, 1, 'SYSTEM', @Now),
    ('MS_PACKAGING', 'func.ms.packaging', 'MS_INVENTORY', 90, 1, 'SYSTEM', @Now),
    ('MS_WAREHOUSE', 'func.ms.warehouse', 'MS_INVENTORY', 100, 1, 'SYSTEM', @Now),
    ('MS_WAREHOUSE_ZONE', 'func.ms.warehouseZone', 'MS_INVENTORY', 110, 1, 'SYSTEM', @Now),
    ('MS_WAREHOUSE_LOCATION', 'func.ms.warehouseLocation', 'MS_INVENTORY', 120, 1, 'SYSTEM', @Now),
    ('MS_LOT_RULE', 'func.ms.lotRule', 'MS_INVENTORY', 130, 1, 'SYSTEM', @Now),
    ('MS_SERIAL_RULE', 'func.ms.serialRule', 'MS_INVENTORY', 140, 1, 'SYSTEM', @Now),
    ('MS_BUYER', 'func.ms.buyer', 'MS_PURCHASING', 10, 1, 'SYSTEM', @Now),
    ('MS_PURCHASE_CATEGORY', 'func.ms.purchaseCategory', 'MS_PURCHASING', 20, 1, 'SYSTEM', @Now),
    ('MS_PAYMENT_TERM', 'func.ms.paymentTerm', 'MS_PURCHASING', 30, 1, 'SYSTEM', @Now),
    ('MS_DELIVERY_TERM', 'func.ms.deliveryTerm', 'MS_PURCHASING', 40, 1, 'SYSTEM', @Now),
    ('MS_INCOTERMS', 'func.ms.incoterms', 'MS_PURCHASING', 50, 1, 'SYSTEM', @Now),
    ('MS_PURCHASE_PRICE_LIST', 'func.ms.purchasePriceList', 'MS_PURCHASING', 60, 1, 'SYSTEM', @Now),
    ('MS_SALESPERSON', 'func.ms.salesperson', 'MS_SALES', 10, 1, 'SYSTEM', @Now),
    ('MS_SALES_REGION', 'func.ms.salesRegion', 'MS_SALES', 20, 1, 'SYSTEM', @Now),
    ('MS_SALES_CHANNEL', 'func.ms.salesChannel', 'MS_SALES', 30, 1, 'SYSTEM', @Now),
    ('MS_CUSTOMER_PRICE_GROUP', 'func.ms.customerPriceGroup', 'MS_SALES', 40, 1, 'SYSTEM', @Now),
    ('MS_SALES_PRICE_LIST', 'func.ms.salesPriceList', 'MS_SALES', 50, 1, 'SYSTEM', @Now),
    ('MS_DISCOUNT_GROUP', 'func.ms.discountGroup', 'MS_SALES', 60, 1, 'SYSTEM', @Now),
    ('MS_DELIVERY_METHOD', 'func.ms.deliveryMethod', 'MS_SALES', 70, 1, 'SYSTEM', @Now),
    ('MS_BOM', 'func.ms.bom', 'MS_PRODUCTION', 10, 1, 'SYSTEM', @Now),
    ('MS_BOM_VERSION', 'func.ms.bomVersion', 'MS_PRODUCTION', 20, 1, 'SYSTEM', @Now),
    ('MS_ROUTING', 'func.ms.routing', 'MS_PRODUCTION', 30, 1, 'SYSTEM', @Now),
    ('MS_ROUTING_VERSION', 'func.ms.routingVersion', 'MS_PRODUCTION', 40, 1, 'SYSTEM', @Now),
    ('MS_OPERATION', 'func.ms.operation', 'MS_PRODUCTION', 50, 1, 'SYSTEM', @Now),
    ('MS_PRODUCTION_LINE', 'func.ms.productionLine', 'MS_PRODUCTION', 60, 1, 'SYSTEM', @Now),
    ('MS_MACHINE', 'func.ms.machine', 'MS_PRODUCTION', 70, 1, 'SYSTEM', @Now),
    ('MS_MOLD', 'func.ms.mold', 'MS_PRODUCTION', 80, 1, 'SYSTEM', @Now),
    ('MS_TOOL', 'func.ms.tool', 'MS_PRODUCTION', 90, 1, 'SYSTEM', @Now),
    ('MS_INSPECTION_ITEM', 'func.ms.inspectionItem', 'MS_QUALITY', 10, 1, 'SYSTEM', @Now),
    ('MS_INSPECTION_STANDARD', 'func.ms.inspectionStandard', 'MS_QUALITY', 20, 1, 'SYSTEM', @Now),
    ('MS_DEFECT_CATEGORY', 'func.ms.defectCategory', 'MS_QUALITY', 30, 1, 'SYSTEM', @Now),
    ('MS_DEFECT_CODE', 'func.ms.defectCode', 'MS_QUALITY', 40, 1, 'SYSTEM', @Now),
    ('MS_SAMPLING_RULE', 'func.ms.samplingRule', 'MS_QUALITY', 50, 1, 'SYSTEM', @Now),
    ('MS_INSPECTION_METHOD', 'func.ms.inspectionMethod', 'MS_QUALITY', 60, 1, 'SYSTEM', @Now),
    ('MS_CHART_OF_ACCOUNTS', 'func.ms.chartOfAccounts', 'MS_FINANCE', 10, 1, 'SYSTEM', @Now),
    ('MS_ACCOUNT_GROUP', 'func.ms.accountGroup', 'MS_FINANCE', 20, 1, 'SYSTEM', @Now),
    ('MS_CURRENCY', 'func.ms.currency', 'MS_FINANCE', 30, 1, 'SYSTEM', @Now),
    ('MS_EXCHANGE_RATE', 'func.ms.exchangeRate', 'MS_FINANCE', 40, 1, 'SYSTEM', @Now),
    ('MS_TAX_CODE', 'func.ms.taxCode', 'MS_FINANCE', 50, 1, 'SYSTEM', @Now),
    ('MS_TAX_RATE', 'func.ms.taxRate', 'MS_FINANCE', 60, 1, 'SYSTEM', @Now),
    ('MS_BANK', 'func.ms.bank', 'MS_FINANCE', 70, 1, 'SYSTEM', @Now),
    ('MS_BANK_BRANCH', 'func.ms.bankBranch', 'MS_FINANCE', 80, 1, 'SYSTEM', @Now),
    ('MS_PAYMENT_METHOD', 'func.ms.paymentMethod', 'MS_FINANCE', 90, 1, 'SYSTEM', @Now),
    ('MS_POSITION', 'func.ms.position', 'MS_HUMAN_RESOURCES', 10, 1, 'SYSTEM', @Now),
    ('MS_JOB_TITLE', 'func.ms.jobTitle', 'MS_HUMAN_RESOURCES', 20, 1, 'SYSTEM', @Now),
    ('MS_EMPLOYEE_TYPE', 'func.ms.employeeType', 'MS_HUMAN_RESOURCES', 30, 1, 'SYSTEM', @Now),
    ('MS_CONTRACT_TYPE', 'func.ms.contractType', 'MS_HUMAN_RESOURCES', 40, 1, 'SYSTEM', @Now),
    ('MS_SHIFT', 'func.ms.shift', 'MS_HUMAN_RESOURCES', 50, 1, 'SYSTEM', @Now),
    ('MS_SHIFT_PATTERN', 'func.ms.shiftPattern', 'MS_HUMAN_RESOURCES', 60, 1, 'SYSTEM', @Now),
    ('MS_LEAVE_TYPE', 'func.ms.leaveType', 'MS_HUMAN_RESOURCES', 70, 1, 'SYSTEM', @Now),
    ('MS_OVERTIME_TYPE', 'func.ms.overtimeType', 'MS_HUMAN_RESOURCES', 80, 1, 'SYSTEM', @Now),
    ('MS_PAYROLL_GROUP', 'func.ms.payrollGroup', 'MS_HUMAN_RESOURCES', 90, 1, 'SYSTEM', @Now),
    ('MS_ASSET_CATEGORY', 'func.ms.assetCategory', 'MS_ASSETS', 10, 1, 'SYSTEM', @Now),
    ('MS_ASSET_TYPE', 'func.ms.assetType', 'MS_ASSETS', 20, 1, 'SYSTEM', @Now),
    ('MS_ASSET_LOCATION', 'func.ms.assetLocation', 'MS_ASSETS', 30, 1, 'SYSTEM', @Now),
    ('MS_DEPRECIATION_METHOD', 'func.ms.depreciationMethod', 'MS_ASSETS', 40, 1, 'SYSTEM', @Now),
    ('MS_PROJECT_TYPE', 'func.ms.projectType', 'MS_PROJECTS', 10, 1, 'SYSTEM', @Now),
    ('MS_PROJECT_STAGE', 'func.ms.projectStage', 'MS_PROJECTS', 20, 1, 'SYSTEM', @Now),
    ('MS_PROJECT_CATEGORY', 'func.ms.projectCategory', 'MS_PROJECTS', 30, 1, 'SYSTEM', @Now),
    ('MS_COUNTRY', 'func.ms.country', 'MS_COMMON', 10, 1, 'SYSTEM', @Now),
    ('MS_STATE_PROVINCE', 'func.ms.stateProvince', 'MS_COMMON', 20, 1, 'SYSTEM', @Now),
    ('MS_CITY', 'func.ms.city', 'MS_COMMON', 30, 1, 'SYSTEM', @Now),
    ('MS_LANGUAGE', 'func.ms.language', 'MS_COMMON', 40, 1, 'SYSTEM', @Now),
    ('MS_TIME_ZONE', 'func.ms.timeZone', 'MS_COMMON', 50, 1, 'SYSTEM', @Now);

INSERT INTO sy_functions (Code, ResourceKey, GroupCode, SortOrder, IsActive, CreatedBy, CreatedAt) VALUES
    ('MS_CALENDAR', 'func.ms.calendar', 'MS_COMMON', 60, 1, 'SYSTEM', @Now),
    ('MS_HOLIDAY', 'func.ms.holiday', 'MS_COMMON', 70, 1, 'SYSTEM', @Now),
    ('MS_REASON', 'func.ms.reason', 'MS_COMMON', 80, 1, 'SYSTEM', @Now),
    ('MS_PRIORITY', 'func.ms.priority', 'MS_COMMON', 90, 1, 'SYSTEM', @Now),
    ('SA_QUOTATIONS', 'func.sa.quotations', 'SA_QUOTATION', 10, 1, 'SYSTEM', @Now),
    ('SA_CREATE_QUOTATION', 'func.sa.createQuotation', 'SA_QUOTATION', 20, 1, 'SYSTEM', @Now),
    ('SA_APPROVE_QUOTATION', 'func.sa.approveQuotation', 'SA_QUOTATION', 30, 1, 'SYSTEM', @Now),
    ('SA_SALES_ORDERS', 'func.sa.salesOrders', 'SA_SALES_ORDER', 10, 1, 'SYSTEM', @Now),
    ('SA_CREATE_SALES_ORDER', 'func.sa.createSalesOrder', 'SA_SALES_ORDER', 20, 1, 'SYSTEM', @Now),
    ('SA_APPROVE_SALES_ORDER', 'func.sa.approveSalesOrder', 'SA_SALES_ORDER', 30, 1, 'SYSTEM', @Now),
    ('SA_DELIVERY_ORDER', 'func.sa.deliveryOrder', 'SA_DELIVERY', 10, 1, 'SYSTEM', @Now),
    ('SA_DELIVERY_CONFIRMATION', 'func.sa.deliveryConfirmation', 'SA_DELIVERY', 20, 1, 'SYSTEM', @Now),
    ('SA_PROOF_OF_DELIVERY', 'func.sa.proofOfDelivery', 'SA_DELIVERY', 30, 1, 'SYSTEM', @Now),
    ('SA_RETURN_REQUEST', 'func.sa.returnRequest', 'SA_RETURN', 10, 1, 'SYSTEM', @Now),
    ('SA_RETURN_RECEIPT', 'func.sa.returnReceipt', 'SA_RETURN', 20, 1, 'SYSTEM', @Now),
    ('SA_CREDIT_NOTE', 'func.sa.creditNote', 'SA_RETURN', 30, 1, 'SYSTEM', @Now),
    ('SA_SALES_INVOICE', 'func.sa.salesInvoice', 'SA_INVOICE', 10, 1, 'SYSTEM', @Now),
    ('SA_INVOICE_ADJUSTMENT', 'func.sa.invoiceAdjustment', 'SA_INVOICE', 20, 1, 'SYSTEM', @Now),
    ('SA_INVOICE_CANCELLATION', 'func.sa.invoiceCancellation', 'SA_INVOICE', 30, 1, 'SYSTEM', @Now),
    ('SA_SALES_ANALYSIS', 'func.sa.salesAnalysis', 'SA_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('SA_PROFIT_ANALYSIS', 'func.sa.profitAnalysis', 'SA_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('SA_OUTSTANDING_ORDERS', 'func.sa.outstandingOrders', 'SA_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('SA_SALES_PERFORMANCE', 'func.sa.salesPerformance', 'SA_REPORTS', 40, 1, 'SYSTEM', @Now),
    ('PU_REQUESTS', 'func.pu.requests', 'PU_PURCHASE_REQUEST', 10, 1, 'SYSTEM', @Now),
    ('PU_CREATE_REQUEST', 'func.pu.createRequest', 'PU_PURCHASE_REQUEST', 20, 1, 'SYSTEM', @Now),
    ('PU_PURCHASE_REQUEST_APPROVAL', 'func.pu.approval', 'PU_PURCHASE_REQUEST', 30, 1, 'SYSTEM', @Now),
    ('PU_REQUEST_FOR_QUOTATION', 'func.pu.requestForQuotation', 'PU_RFQ', 10, 1, 'SYSTEM', @Now),
    ('PU_QUOTATION_COMPARISON', 'func.pu.quotationComparison', 'PU_RFQ', 20, 1, 'SYSTEM', @Now),
    ('PU_SUPPLIER_SELECTION', 'func.pu.supplierSelection', 'PU_RFQ', 30, 1, 'SYSTEM', @Now),
    ('PU_PURCHASE_ORDERS', 'func.pu.purchaseOrders', 'PU_PURCHASE_ORDER', 10, 1, 'SYSTEM', @Now),
    ('PU_CREATE_PURCHASE_ORDER', 'func.pu.createPurchaseOrder', 'PU_PURCHASE_ORDER', 20, 1, 'SYSTEM', @Now),
    ('PU_PURCHASE_ORDER_APPROVAL', 'func.pu.approval', 'PU_PURCHASE_ORDER', 30, 1, 'SYSTEM', @Now),
    ('PU_GOODS_RECEIPT_NOTE', 'func.pu.goodsReceiptNote', 'PU_GOODS_RECEIPT', 10, 1, 'SYSTEM', @Now),
    ('PU_RECEIVING_INSPECTION', 'func.pu.receivingInspection', 'PU_GOODS_RECEIPT', 20, 1, 'SYSTEM', @Now),
    ('PU_STOCK_RECEIPT', 'func.pu.stockReceipt', 'PU_GOODS_RECEIPT', 30, 1, 'SYSTEM', @Now),
    ('PU_RETURN_REQUEST', 'func.pu.returnRequest', 'PU_SUPPLIER_RETURN', 10, 1, 'SYSTEM', @Now),
    ('PU_RETURN_SHIPMENT', 'func.pu.returnShipment', 'PU_SUPPLIER_RETURN', 20, 1, 'SYSTEM', @Now),
    ('PU_SUPPLIER_CREDIT', 'func.pu.supplierCredit', 'PU_SUPPLIER_RETURN', 30, 1, 'SYSTEM', @Now),
    ('PU_PURCHASE_ANALYSIS', 'func.pu.purchaseAnalysis', 'PU_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('PU_SUPPLIER_PERFORMANCE', 'func.pu.supplierPerformance', 'PU_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('PU_DELIVERY_PERFORMANCE', 'func.pu.deliveryPerformance', 'PU_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('PU_OUTSTANDING_PURCHASE_ORDERS', 'func.pu.outstandingPurchaseOrders', 'PU_REPORTS', 40, 1, 'SYSTEM', @Now),
    ('IV_PURCHASE_RECEIPT', 'func.iv.purchaseReceipt', 'IV_RECEIVING', 10, 1, 'SYSTEM', @Now),
    ('IV_PRODUCTION_RECEIPT', 'func.iv.productionReceipt', 'IV_RECEIVING', 20, 1, 'SYSTEM', @Now),
    ('IV_RETURN_RECEIPT', 'func.iv.returnReceipt', 'IV_RECEIVING', 30, 1, 'SYSTEM', @Now),
    ('IV_MISCELLANEOUS_RECEIPT', 'func.iv.miscellaneousReceipt', 'IV_RECEIVING', 40, 1, 'SYSTEM', @Now),
    ('IV_SALES_ISSUE', 'func.iv.salesIssue', 'IV_ISSUING', 10, 1, 'SYSTEM', @Now),
    ('IV_PRODUCTION_ISSUE', 'func.iv.productionIssue', 'IV_ISSUING', 20, 1, 'SYSTEM', @Now),
    ('IV_RETURN_ISSUE', 'func.iv.returnIssue', 'IV_ISSUING', 30, 1, 'SYSTEM', @Now),
    ('IV_MISCELLANEOUS_ISSUE', 'func.iv.miscellaneousIssue', 'IV_ISSUING', 40, 1, 'SYSTEM', @Now),
    ('IV_TRANSFER_REQUEST', 'func.iv.transferRequest', 'IV_TRANSFER', 10, 1, 'SYSTEM', @Now),
    ('IV_STOCK_TRANSFER', 'func.iv.stockTransfer', 'IV_TRANSFER', 20, 1, 'SYSTEM', @Now),
    ('IV_TRANSFER_CONFIRMATION', 'func.iv.transferConfirmation', 'IV_TRANSFER', 30, 1, 'SYSTEM', @Now),
    ('IV_COUNT_PLAN', 'func.iv.countPlan', 'IV_STOCK_COUNT', 10, 1, 'SYSTEM', @Now),
    ('IV_STOCK_COUNT', 'func.iv.stockCount', 'IV_STOCK_COUNT', 20, 1, 'SYSTEM', @Now),
    ('IV_STOCK_ADJUSTMENT', 'func.iv.stockAdjustment', 'IV_STOCK_COUNT', 30, 1, 'SYSTEM', @Now),
    ('IV_LOT_TRACKING', 'func.iv.lotTracking', 'IV_LOT_SERIAL', 10, 1, 'SYSTEM', @Now),
    ('IV_SERIAL_TRACKING', 'func.iv.serialTracking', 'IV_LOT_SERIAL', 20, 1, 'SYSTEM', @Now),
    ('IV_TRACEABILITY', 'func.iv.traceability', 'IV_LOT_SERIAL', 30, 1, 'SYSTEM', @Now),
    ('IV_STOCK_BALANCE', 'func.iv.stockBalance', 'IV_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('IV_INVENTORY_MOVEMENT', 'func.iv.inventoryMovement', 'IV_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('IV_INVENTORY_AGING', 'func.iv.inventoryAging', 'IV_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('IV_EXPIRING_INVENTORY', 'func.iv.expiringInventory', 'IV_REPORTS', 40, 1, 'SYSTEM', @Now),
    ('PR_PRODUCTION_PLAN', 'func.pr.productionPlan', 'PR_PLANNING', 10, 1, 'SYSTEM', @Now),
    ('PR_MRP', 'func.pr.mrp', 'PR_PLANNING', 20, 1, 'SYSTEM', @Now),
    ('PR_CAPACITY_PLANNING', 'func.pr.capacityPlanning', 'PR_PLANNING', 30, 1, 'SYSTEM', @Now),
    ('PR_PRODUCTION_SCHEDULE', 'func.pr.productionSchedule', 'PR_PLANNING', 40, 1, 'SYSTEM', @Now),
    ('PR_PRODUCTION_ORDERS', 'func.pr.productionOrders', 'PR_PRODUCTION_ORDER', 10, 1, 'SYSTEM', @Now),
    ('PR_RELEASE_ORDER', 'func.pr.releaseOrder', 'PR_PRODUCTION_ORDER', 20, 1, 'SYSTEM', @Now),
    ('PR_CLOSE_ORDER', 'func.pr.closeOrder', 'PR_PRODUCTION_ORDER', 30, 1, 'SYSTEM', @Now),
    ('PR_MATERIAL_REQUEST', 'func.pr.materialRequest', 'PR_MATERIAL_ISSUE', 10, 1, 'SYSTEM', @Now),
    ('PR_MATERIAL_ISSUE', 'func.pr.materialIssue', 'PR_MATERIAL_ISSUE', 20, 1, 'SYSTEM', @Now),
    ('PR_MATERIAL_RETURN', 'func.pr.materialReturn', 'PR_MATERIAL_ISSUE', 30, 1, 'SYSTEM', @Now),
    ('PR_PRODUCTION_REPORT', 'func.pr.productionReport', 'PR_SHOP_FLOOR', 10, 1, 'SYSTEM', @Now),
    ('PR_SCRAP_REPORT', 'func.pr.scrapReport', 'PR_SHOP_FLOOR', 20, 1, 'SYSTEM', @Now),
    ('PR_MACHINE_TIME', 'func.pr.machineTime', 'PR_SHOP_FLOOR', 30, 1, 'SYSTEM', @Now),
    ('PR_LABOR_TIME', 'func.pr.laborTime', 'PR_SHOP_FLOOR', 40, 1, 'SYSTEM', @Now),
    ('PR_FINISHED_GOODS_RECEIPT', 'func.pr.finishedGoodsReceipt', 'PR_FINISHED_GOODS', 10, 1, 'SYSTEM', @Now),
    ('PR_SEMI_FINISHED_GOODS', 'func.pr.semiFinishedGoods', 'PR_FINISHED_GOODS', 20, 1, 'SYSTEM', @Now),
    ('PR_PACKAGING', 'func.pr.packaging', 'PR_FINISHED_GOODS', 30, 1, 'SYSTEM', @Now),
    ('PR_PRODUCTION_OUTPUT', 'func.pr.productionOutput', 'PR_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('PR_MACHINE_EFFICIENCY', 'func.pr.machineEfficiency', 'PR_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('PR_SCRAP_ANALYSIS', 'func.pr.scrapAnalysis', 'PR_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('PR_PRODUCTION_PROGRESS', 'func.pr.productionProgress', 'PR_REPORTS', 40, 1, 'SYSTEM', @Now),
    ('QM_INSPECTION_PLAN', 'func.qm.inspectionPlan', 'QM_INCOMING_INSPECTION', 10, 1, 'SYSTEM', @Now),
    ('QM_INSPECTION_RESULT', 'func.qm.inspectionResult', 'QM_INCOMING_INSPECTION', 20, 1, 'SYSTEM', @Now),
    ('QM_ACCEPTANCE', 'func.qm.acceptance', 'QM_INCOMING_INSPECTION', 30, 1, 'SYSTEM', @Now),
    ('QM_PROCESS_INSPECTION', 'func.qm.processInspection', 'QM_IN_PROCESS_INSPECTION', 10, 1, 'SYSTEM', @Now),
    ('QM_DEFECT_RECORDING', 'func.qm.defectRecording', 'QM_IN_PROCESS_INSPECTION', 20, 1, 'SYSTEM', @Now),
    ('QM_CORRECTIVE_ACTION', 'func.qm.correctiveAction', 'QM_IN_PROCESS_INSPECTION', 30, 1, 'SYSTEM', @Now),
    ('QM_FINAL_INSPECTION', 'func.qm.finalInspection', 'QM_FINAL_INSPECTION', 10, 1, 'SYSTEM', @Now),
    ('QM_QUALITY_CERTIFICATE', 'func.qm.qualityCertificate', 'QM_FINAL_INSPECTION', 20, 1, 'SYSTEM', @Now),
    ('QM_SHIPMENT_APPROVAL', 'func.qm.shipmentApproval', 'QM_FINAL_INSPECTION', 30, 1, 'SYSTEM', @Now),
    ('QM_NCR', 'func.qm.ncr', 'QM_NON_CONFORMANCE', 10, 1, 'SYSTEM', @Now),
    ('QM_ROOT_CAUSE_ANALYSIS', 'func.qm.rootCauseAnalysis', 'QM_NON_CONFORMANCE', 20, 1, 'SYSTEM', @Now),
    ('QM_CAPA', 'func.qm.capa', 'QM_NON_CONFORMANCE', 30, 1, 'SYSTEM', @Now),
    ('QM_QUALITY_PERFORMANCE', 'func.qm.qualityPerformance', 'QM_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('QM_DEFECT_ANALYSIS', 'func.qm.defectAnalysis', 'QM_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('QM_SUPPLIER_QUALITY', 'func.qm.supplierQuality', 'QM_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('QM_COST_OF_QUALITY', 'func.qm.costOfQuality', 'QM_REPORTS', 40, 1, 'SYSTEM', @Now);

INSERT INTO sy_functions (Code, ResourceKey, GroupCode, SortOrder, IsActive, CreatedBy, CreatedAt) VALUES
    ('FI_JOURNAL_ENTRY', 'func.fi.journalEntry', 'FI_GENERAL_LEDGER', 10, 1, 'SYSTEM', @Now),
    ('FI_CLOSING', 'func.fi.closing', 'FI_GENERAL_LEDGER', 20, 1, 'SYSTEM', @Now),
    ('FI_FISCAL_PERIOD', 'func.fi.fiscalPeriod', 'FI_GENERAL_LEDGER', 30, 1, 'SYSTEM', @Now),
    ('FI_CUSTOMER_INVOICE', 'func.fi.customerInvoice', 'FI_ACCOUNTS_RECEIVABLE', 10, 1, 'SYSTEM', @Now),
    ('FI_CUSTOMER_RECEIPT', 'func.fi.customerReceipt', 'FI_ACCOUNTS_RECEIVABLE', 20, 1, 'SYSTEM', @Now),
    ('FI_AR_AGING', 'func.fi.arAging', 'FI_ACCOUNTS_RECEIVABLE', 30, 1, 'SYSTEM', @Now),
    ('FI_SUPPLIER_INVOICE', 'func.fi.supplierInvoice', 'FI_ACCOUNTS_PAYABLE', 10, 1, 'SYSTEM', @Now),
    ('FI_SUPPLIER_PAYMENT', 'func.fi.supplierPayment', 'FI_ACCOUNTS_PAYABLE', 20, 1, 'SYSTEM', @Now),
    ('FI_AP_AGING', 'func.fi.apAging', 'FI_ACCOUNTS_PAYABLE', 30, 1, 'SYSTEM', @Now),
    ('FI_CASH_RECEIPT', 'func.fi.cashReceipt', 'FI_CASH', 10, 1, 'SYSTEM', @Now),
    ('FI_CASH_PAYMENT', 'func.fi.cashPayment', 'FI_CASH', 20, 1, 'SYSTEM', @Now),
    ('FI_CASH_BOOK', 'func.fi.cashBook', 'FI_CASH', 30, 1, 'SYSTEM', @Now),
    ('FI_BANK_RECEIPT', 'func.fi.bankReceipt', 'FI_BANK', 10, 1, 'SYSTEM', @Now),
    ('FI_BANK_PAYMENT', 'func.fi.bankPayment', 'FI_BANK', 20, 1, 'SYSTEM', @Now),
    ('FI_BANK_RECONCILIATION', 'func.fi.bankReconciliation', 'FI_BANK', 30, 1, 'SYSTEM', @Now),
    ('FI_ASSET_REGISTER', 'func.fi.assetRegister', 'FI_FIXED_ASSETS', 10, 1, 'SYSTEM', @Now),
    ('FI_DEPRECIATION', 'func.fi.depreciation', 'FI_FIXED_ASSETS', 20, 1, 'SYSTEM', @Now),
    ('FI_ASSET_DISPOSAL', 'func.fi.assetDisposal', 'FI_FIXED_ASSETS', 30, 1, 'SYSTEM', @Now),
    ('FI_BUDGET_PLANNING', 'func.fi.budgetPlanning', 'FI_BUDGET', 10, 1, 'SYSTEM', @Now),
    ('FI_BUDGET_ALLOCATION', 'func.fi.budgetAllocation', 'FI_BUDGET', 20, 1, 'SYSTEM', @Now),
    ('FI_BUDGET_CONTROL', 'func.fi.budgetControl', 'FI_BUDGET', 30, 1, 'SYSTEM', @Now),
    ('FI_BALANCE_SHEET', 'func.fi.balanceSheet', 'FI_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('FI_INCOME_STATEMENT', 'func.fi.incomeStatement', 'FI_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('FI_CASH_FLOW', 'func.fi.cashFlow', 'FI_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('FI_GENERAL_LEDGER', 'func.fi.generalLedger', 'FI_REPORTS', 40, 1, 'SYSTEM', @Now),
    ('HR_EMPLOYEE', 'func.hr.employee', 'HR_EMPLOYEE', 10, 1, 'SYSTEM', @Now),
    ('HR_EMPLOYEE_PROFILE', 'func.hr.employeeProfile', 'HR_EMPLOYEE', 20, 1, 'SYSTEM', @Now),
    ('HR_EMPLOYMENT_CONTRACT', 'func.hr.employmentContract', 'HR_EMPLOYEE', 30, 1, 'SYSTEM', @Now),
    ('HR_RECRUITMENT_REQUEST', 'func.hr.recruitmentRequest', 'HR_RECRUITMENT', 10, 1, 'SYSTEM', @Now),
    ('HR_CANDIDATE', 'func.hr.candidate', 'HR_RECRUITMENT', 20, 1, 'SYSTEM', @Now),
    ('HR_INTERVIEW', 'func.hr.interview', 'HR_RECRUITMENT', 30, 1, 'SYSTEM', @Now),
    ('HR_ATTENDANCE_LOG', 'func.hr.attendanceLog', 'HR_ATTENDANCE', 10, 1, 'SYSTEM', @Now),
    ('HR_ATTENDANCE_SHEET', 'func.hr.attendanceSheet', 'HR_ATTENDANCE', 20, 1, 'SYSTEM', @Now),
    ('HR_ATTENDANCE_ADJUSTMENT', 'func.hr.attendanceAdjustment', 'HR_ATTENDANCE', 30, 1, 'SYSTEM', @Now),
    ('HR_LEAVE_REQUEST', 'func.hr.leaveRequest', 'HR_LEAVE', 10, 1, 'SYSTEM', @Now),
    ('HR_LEAVE_APPROVAL', 'func.hr.leaveApproval', 'HR_LEAVE', 20, 1, 'SYSTEM', @Now),
    ('HR_LEAVE_BALANCE', 'func.hr.leaveBalance', 'HR_LEAVE', 30, 1, 'SYSTEM', @Now),
    ('HR_OVERTIME_REQUEST', 'func.hr.overtimeRequest', 'HR_OVERTIME', 10, 1, 'SYSTEM', @Now),
    ('HR_OVERTIME_APPROVAL', 'func.hr.overtimeApproval', 'HR_OVERTIME', 20, 1, 'SYSTEM', @Now),
    ('HR_OVERTIME_SUMMARY', 'func.hr.overtimeSummary', 'HR_OVERTIME', 30, 1, 'SYSTEM', @Now),
    ('HR_PAYROLL_CALCULATION', 'func.hr.payrollCalculation', 'HR_PAYROLL', 10, 1, 'SYSTEM', @Now),
    ('HR_PAYSLIP', 'func.hr.payslip', 'HR_PAYROLL', 20, 1, 'SYSTEM', @Now),
    ('HR_PAYROLL_ADJUSTMENT', 'func.hr.payrollAdjustment', 'HR_PAYROLL', 30, 1, 'SYSTEM', @Now),
    ('HR_KPI', 'func.hr.kpi', 'HR_PERFORMANCE', 10, 1, 'SYSTEM', @Now),
    ('HR_PERFORMANCE_REVIEW', 'func.hr.performanceReview', 'HR_PERFORMANCE', 20, 1, 'SYSTEM', @Now),
    ('HR_EMPLOYEE_EVALUATION', 'func.hr.employeeEvaluation', 'HR_PERFORMANCE', 30, 1, 'SYSTEM', @Now),
    ('HR_TRAINING_PLAN', 'func.hr.trainingPlan', 'HR_TRAINING', 10, 1, 'SYSTEM', @Now),
    ('HR_TRAINING_COURSE', 'func.hr.trainingCourse', 'HR_TRAINING', 20, 1, 'SYSTEM', @Now),
    ('HR_CERTIFICATION', 'func.hr.certification', 'HR_TRAINING', 30, 1, 'SYSTEM', @Now),
    ('HR_HEADCOUNT', 'func.hr.headcount', 'HR_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('HR_TURNOVER', 'func.hr.turnover', 'HR_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('HR_PAYROLL_SUMMARY', 'func.hr.payrollSummary', 'HR_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('HR_ATTENDANCE_SUMMARY', 'func.hr.attendanceSummary', 'HR_REPORTS', 40, 1, 'SYSTEM', @Now),
    ('CR_LEAD', 'func.cr.lead', 'CR_LEAD', 10, 1, 'SYSTEM', @Now),
    ('CR_ASSIGNMENT', 'func.cr.assignment', 'CR_LEAD', 20, 1, 'SYSTEM', @Now),
    ('CR_CONVERSION', 'func.cr.conversion', 'CR_LEAD', 30, 1, 'SYSTEM', @Now),
    ('CR_OPPORTUNITY', 'func.cr.opportunity', 'CR_OPPORTUNITY', 10, 1, 'SYSTEM', @Now),
    ('CR_SALES_PIPELINE', 'func.cr.salesPipeline', 'CR_OPPORTUNITY', 20, 1, 'SYSTEM', @Now),
    ('CR_FORECAST', 'func.cr.forecast', 'CR_OPPORTUNITY', 30, 1, 'SYSTEM', @Now),
    ('CR_CALL', 'func.cr.call', 'CR_ACTIVITIES', 10, 1, 'SYSTEM', @Now),
    ('CR_MEETING', 'func.cr.meeting', 'CR_ACTIVITIES', 20, 1, 'SYSTEM', @Now),
    ('CR_TASK', 'func.cr.task', 'CR_ACTIVITIES', 30, 1, 'SYSTEM', @Now),
    ('CR_CAMPAIGN', 'func.cr.campaign', 'CR_CAMPAIGN', 10, 1, 'SYSTEM', @Now),
    ('CR_TARGET', 'func.cr.target', 'CR_CAMPAIGN', 20, 1, 'SYSTEM', @Now),
    ('CR_CAMPAIGN_RESULT', 'func.cr.campaignResult', 'CR_CAMPAIGN', 30, 1, 'SYSTEM', @Now),
    ('CR_SUPPORT_TICKET', 'func.cr.supportTicket', 'CR_CUSTOMER_SERVICE', 10, 1, 'SYSTEM', @Now),
    ('CR_CUSTOMER_COMPLAINT', 'func.cr.customerComplaint', 'CR_CUSTOMER_SERVICE', 20, 1, 'SYSTEM', @Now),
    ('CR_INTERACTION_HISTORY', 'func.cr.interactionHistory', 'CR_CUSTOMER_SERVICE', 30, 1, 'SYSTEM', @Now),
    ('PM_PROJECT', 'func.pm.project', 'PM_PROJECT', 10, 1, 'SYSTEM', @Now),
    ('PM_CREATE_PROJECT', 'func.pm.createProject', 'PM_PROJECT', 20, 1, 'SYSTEM', @Now),
    ('PM_CLOSE_PROJECT', 'func.pm.closeProject', 'PM_PROJECT', 30, 1, 'SYSTEM', @Now),
    ('PM_WBS', 'func.pm.wbs', 'PM_PLANNING', 10, 1, 'SYSTEM', @Now),
    ('PM_MILESTONE', 'func.pm.milestone', 'PM_PLANNING', 20, 1, 'SYSTEM', @Now),
    ('PM_RESOURCE_PLAN', 'func.pm.resourcePlan', 'PM_PLANNING', 30, 1, 'SYSTEM', @Now),
    ('PM_TASK', 'func.pm.task', 'PM_EXECUTION', 10, 1, 'SYSTEM', @Now),
    ('PM_TIMESHEET', 'func.pm.timesheet', 'PM_EXECUTION', 20, 1, 'SYSTEM', @Now),
    ('PM_ISSUE', 'func.pm.issue', 'PM_EXECUTION', 30, 1, 'SYSTEM', @Now),
    ('PM_BUDGET', 'func.pm.budget', 'PM_COST', 10, 1, 'SYSTEM', @Now),
    ('PM_ACTUAL_COST', 'func.pm.actualCost', 'PM_COST', 20, 1, 'SYSTEM', @Now),
    ('PM_REVENUE', 'func.pm.revenue', 'PM_COST', 30, 1, 'SYSTEM', @Now),
    ('PM_PROJECT_PROGRESS', 'func.pm.projectProgress', 'PM_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('PM_RESOURCE_UTILIZATION', 'func.pm.resourceUtilization', 'PM_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('PM_PROJECT_COST', 'func.pm.projectCost', 'PM_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('AM_ASSET_REGISTER', 'func.am.assetRegister', 'AM_ASSETS', 10, 1, 'SYSTEM', @Now),
    ('AM_ASSET_TRANSFER', 'func.am.assetTransfer', 'AM_ASSETS', 20, 1, 'SYSTEM', @Now),
    ('AM_ASSET_DISPOSAL', 'func.am.assetDisposal', 'AM_ASSETS', 30, 1, 'SYSTEM', @Now),
    ('AM_ASSET_ASSIGNMENT', 'func.am.assetAssignment', 'AM_ASSIGNMENT', 10, 1, 'SYSTEM', @Now),
    ('AM_ASSET_RETURN', 'func.am.assetReturn', 'AM_ASSIGNMENT', 20, 1, 'SYSTEM', @Now),
    ('AM_ASSET_HANDOVER', 'func.am.assetHandover', 'AM_ASSIGNMENT', 30, 1, 'SYSTEM', @Now),
    ('AM_MAINTENANCE_PLAN', 'func.am.maintenancePlan', 'AM_MAINTENANCE', 10, 1, 'SYSTEM', @Now),
    ('AM_WORK_ORDER', 'func.am.workOrder', 'AM_MAINTENANCE', 20, 1, 'SYSTEM', @Now),
    ('AM_MAINTENANCE_HISTORY', 'func.am.maintenanceHistory', 'AM_MAINTENANCE', 30, 1, 'SYSTEM', @Now),
    ('AM_ASSET_COUNT', 'func.am.assetCount', 'AM_INVENTORY', 10, 1, 'SYSTEM', @Now),
    ('AM_ASSET_ADJUSTMENT', 'func.am.assetAdjustment', 'AM_INVENTORY', 20, 1, 'SYSTEM', @Now),
    ('AM_ASSET_STATUS', 'func.am.assetStatus', 'AM_INVENTORY', 30, 1, 'SYSTEM', @Now),
    ('AM_ASSET_SUMMARY', 'func.am.assetSummary', 'AM_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('AM_DEPRECIATION', 'func.am.depreciation', 'AM_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('AM_MAINTENANCE_COST', 'func.am.maintenanceCost', 'AM_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('WF_MY_REQUESTS', 'func.wf.myRequests', 'WF_APPROVAL', 10, 1, 'SYSTEM', @Now),
    ('WF_PENDING_APPROVAL', 'func.wf.pendingApproval', 'WF_APPROVAL', 20, 1, 'SYSTEM', @Now);

INSERT INTO sy_functions (Code, ResourceKey, GroupCode, SortOrder, IsActive, CreatedBy, CreatedAt) VALUES
    ('WF_APPROVAL_HISTORY', 'func.wf.approvalHistory', 'WF_APPROVAL', 30, 1, 'SYSTEM', @Now),
    ('WF_WORKFLOW_DEFINITION', 'func.wf.workflowDefinition', 'WF_WORKFLOW', 10, 1, 'SYSTEM', @Now),
    ('WF_APPROVAL_LEVEL', 'func.wf.approvalLevel', 'WF_WORKFLOW', 20, 1, 'SYSTEM', @Now),
    ('WF_APPROVER', 'func.wf.approver', 'WF_WORKFLOW', 30, 1, 'SYSTEM', @Now),
    ('WF_DELEGATE_APPROVAL', 'func.wf.delegateApproval', 'WF_DELEGATION', 10, 1, 'SYSTEM', @Now),
    ('WF_ACTIVE_DELEGATIONS', 'func.wf.activeDelegations', 'WF_DELEGATION', 20, 1, 'SYSTEM', @Now),
    ('WF_DELEGATION_HISTORY', 'func.wf.delegationHistory', 'WF_DELEGATION', 30, 1, 'SYSTEM', @Now),
    ('DM_DOCUMENT', 'func.dm.document', 'DM_DOCUMENTS', 10, 1, 'SYSTEM', @Now),
    ('DM_UPLOAD', 'func.dm.upload', 'DM_DOCUMENTS', 20, 1, 'SYSTEM', @Now),
    ('DM_VERSION_CONTROL', 'func.dm.versionControl', 'DM_DOCUMENTS', 30, 1, 'SYSTEM', @Now),
    ('DM_DOCUMENT_CATEGORY', 'func.dm.documentCategory', 'DM_CATEGORIES', 10, 1, 'SYSTEM', @Now),
    ('DM_DOCUMENT_TYPE', 'func.dm.documentType', 'DM_CATEGORIES', 20, 1, 'SYSTEM', @Now),
    ('DM_TAGS', 'func.dm.tags', 'DM_CATEGORIES', 30, 1, 'SYSTEM', @Now),
    ('DM_PENDING_DOCUMENTS', 'func.dm.pendingDocuments', 'DM_APPROVAL', 10, 1, 'SYSTEM', @Now),
    ('DM_APPROVED_DOCUMENTS', 'func.dm.approvedDocuments', 'DM_APPROVAL', 20, 1, 'SYSTEM', @Now),
    ('DM_EXPIRED_DOCUMENTS', 'func.dm.expiredDocuments', 'DM_APPROVAL', 30, 1, 'SYSTEM', @Now),
    ('DM_SEARCH_DOCUMENTS', 'func.dm.searchDocuments', 'DM_SEARCH', 10, 1, 'SYSTEM', @Now),
    ('DM_RECENT_DOCUMENTS', 'func.dm.recentDocuments', 'DM_SEARCH', 20, 1, 'SYSTEM', @Now),
    ('DM_MY_DOCUMENTS', 'func.dm.myDocuments', 'DM_SEARCH', 30, 1, 'SYSTEM', @Now),
    ('RP_SALES', 'func.rp.sales', 'RP_OPERATIONAL_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('RP_PURCHASING', 'func.rp.purchasing', 'RP_OPERATIONAL_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('RP_INVENTORY', 'func.rp.inventory', 'RP_OPERATIONAL_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('RP_PRODUCTION', 'func.rp.production', 'RP_OPERATIONAL_REPORTS', 40, 1, 'SYSTEM', @Now),
    ('RP_AR_AP', 'func.rp.arAp', 'RP_FINANCIAL_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('RP_CASH_FLOW', 'func.rp.cashFlow', 'RP_FINANCIAL_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('RP_PROFIT_LOSS', 'func.rp.profitLoss', 'RP_FINANCIAL_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('RP_BALANCE_SHEET', 'func.rp.balanceSheet', 'RP_FINANCIAL_REPORTS', 40, 1, 'SYSTEM', @Now),
    ('RP_KPI_DASHBOARD', 'func.rp.kpiDashboard', 'RP_MANAGEMENT_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('RP_PERFORMANCE_ANALYSIS', 'func.rp.performanceAnalysis', 'RP_MANAGEMENT_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('RP_EXECUTIVE_REPORTS', 'func.rp.executiveReports', 'RP_MANAGEMENT_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('RP_REPORT_DESIGNER', 'func.rp.reportDesigner', 'RP_CUSTOM_REPORTS', 10, 1, 'SYSTEM', @Now),
    ('RP_SCHEDULED_REPORTS', 'func.rp.scheduledReports', 'RP_CUSTOM_REPORTS', 20, 1, 'SYSTEM', @Now),
    ('RP_REPORT_LIBRARY', 'func.rp.reportLibrary', 'RP_CUSTOM_REPORTS', 30, 1, 'SYSTEM', @Now),
    ('SY_USER', 'func.sy.user', 'SY_SECURITY', 10, 1, 'SYSTEM', @Now),
    ('SY_USER_GROUP', 'func.sy.userGroup', 'SY_SECURITY', 20, 1, 'SYSTEM', @Now),
    ('SY_ROLE', 'func.sy.role', 'SY_SECURITY', 30, 1, 'SYSTEM', @Now),
    ('SY_PERMISSION', 'func.sy.permission', 'SY_SECURITY', 40, 1, 'SYSTEM', @Now),
    ('SY_MENU', 'func.sy.menu', 'SY_NAVIGATION', 10, 1, 'SYSTEM', @Now),
    ('SY_FUNCTION', 'func.sy.function', 'SY_NAVIGATION', 20, 1, 'SYSTEM', @Now),
    ('SY_ACTION', 'func.sy.action', 'SY_NAVIGATION', 30, 1, 'SYSTEM', @Now),
    ('SY_SYSTEM_PARAMETERS', 'func.sy.systemParameters', 'SY_CONFIGURATION', 10, 1, 'SYSTEM', @Now),
    ('SY_COMPANY_SETTINGS', 'func.sy.companySettings', 'SY_CONFIGURATION', 20, 1, 'SYSTEM', @Now),
    ('SY_EMAIL_SETTINGS', 'func.sy.emailSettings', 'SY_CONFIGURATION', 30, 1, 'SYSTEM', @Now),
    ('SY_STORAGE_SETTINGS', 'func.sy.storageSettings', 'SY_CONFIGURATION', 40, 1, 'SYSTEM', @Now),
    ('SY_DOCUMENT_TYPE', 'func.sy.documentType', 'SY_NUMBERING', 10, 1, 'SYSTEM', @Now),
    ('SY_NUMBERING_RULE', 'func.sy.numberingRule', 'SY_NUMBERING', 20, 1, 'SYSTEM', @Now),
    ('SY_SEQUENCE', 'func.sy.sequence', 'SY_NUMBERING', 30, 1, 'SYSTEM', @Now),
    ('SY_REPORT_TEMPLATE', 'func.sy.reportTemplate', 'SY_TEMPLATES', 10, 1, 'SYSTEM', @Now),
    ('SY_EMAIL_TEMPLATE', 'func.sy.emailTemplate', 'SY_TEMPLATES', 20, 1, 'SYSTEM', @Now),
    ('SY_NOTIFICATION_TEMPLATE', 'func.sy.notificationTemplate', 'SY_TEMPLATES', 30, 1, 'SYSTEM', @Now),
    ('SY_IMPORT_EXPORT_TEMPLATE', 'func.sy.importExportTemplate', 'SY_TEMPLATES', 40, 1, 'SYSTEM', @Now),
    ('SY_API_CLIENT', 'func.sy.apiClient', 'SY_INTEGRATION', 10, 1, 'SYSTEM', @Now),
    ('SY_WEBHOOK', 'func.sy.webhook', 'SY_INTEGRATION', 20, 1, 'SYSTEM', @Now),
    ('SY_DATA_SYNCHRONIZATION', 'func.sy.dataSynchronization', 'SY_INTEGRATION', 30, 1, 'SYSTEM', @Now),
    ('SY_AUDIT_LOG', 'func.sy.auditLog', 'SY_AUDIT', 10, 1, 'SYSTEM', @Now),
    ('SY_LOGIN_LOG', 'func.sy.loginLog', 'SY_AUDIT', 20, 1, 'SYSTEM', @Now),
    ('SY_ERROR_LOG', 'func.sy.errorLog', 'SY_AUDIT', 30, 1, 'SYSTEM', @Now),
    ('SY_CHANGE_LOG', 'func.sy.changeLog', 'SY_AUDIT', 40, 1, 'SYSTEM', @Now),
    ('SY_DATA_IMPORT', 'func.sy.dataImport', 'SY_UTILITIES', 10, 1, 'SYSTEM', @Now),
    ('SY_DATA_EXPORT', 'func.sy.dataExport', 'SY_UTILITIES', 20, 1, 'SYSTEM', @Now),
    ('SY_BACKUP', 'func.sy.backup', 'SY_UTILITIES', 30, 1, 'SYSTEM', @Now),
    ('SY_RESTORE', 'func.sy.restore', 'SY_UTILITIES', 40, 1, 'SYSTEM', @Now);

GO
