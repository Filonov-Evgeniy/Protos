CREATE TABLE [dbo].[Operation_Type] (
    [id]          INT           NOT NULL,
    [name]        VARCHAR (100) NOT NULL,
    [description] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[Operation] (
    [id]             INT           NOT NULL,
    [code]           INT           NOT NULL,
    [type_id]        INT           NOT NULL,
    [product_id]     INT           NOT NULL,
    [coop_status_id] INT           NOT NULL,
    [description]    VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([product_id]) REFERENCES [dbo].[Product] ([id]),
    FOREIGN KEY ([code]) REFERENCES [dbo].[Operation_Type] ([id])
);

INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (-1, N'Default', N'')
INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (1, N'Сборка-сварка', NULL)
INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (2, N'Токарная', NULL)
INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (3, N'Сверление', NULL)
INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (4, N'Фрезерная', NULL)
INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (5, N'Сборка', NULL)
INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (6, N'Транспорт 1', NULL)
INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (7, N'Транспорт 2', NULL)
INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (8, N'Загрузка Д', NULL)
INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (9, N'Загрузка П', NULL)
INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (10, N'Очистка', NULL)
INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (11, N'Выгрузка', NULL)
INSERT [dbo].[Operation_Type] ([id], [name], [description]) VALUES (12, N'Выкройка', NULL)

INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (1, 5, 2, 1, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (4, 10, 3, 1, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (6, 5, 4, 2, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (8, 10, 3, 2, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (10, 5, 2, 3, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (12, 5, 4, 4, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (14, 10, 3, 4, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (16, 5, 2, 5, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (19, 10, 4, 5, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (22, 5, 5, 10, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (24, 5, 2, 6, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (26, 10, 3, 6, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (28, 5, 4, 7, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (30, 10, 3, 7, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (32, 5, 2, 8, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (34, 10, 4, 8, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (70, 5, 6, 205, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (71, 10, 8, 205, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (72, 15, 10, 205, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (73, 20, 11, 205, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (74, 25, 7, 205, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (75, 30, 9, 205, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (76, 35, 12, 205, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (77, 5, 6, 206, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (78, 10, 8, 206, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (79, 15, 10, 206, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (80, 20, 11, 206, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (81, 25, 7, 206, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (82, 30, 9, 206, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (83, 35, 12, 206, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (85, 5, 5, 12, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (86, 5, 1, 9, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (87, 5, 1, 11, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (88, 5, 6, 202, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (89, 10, 8, 202, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (90, 15, 10, 202, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (91, 20, 11, 202, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (92, 25, 7, 202, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (93, 30, 9, 202, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (94, 35, 12, 202, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (95, 5, 6, 203, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (96, 10, 8, 203, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (97, 15, 10, 203, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (98, 20, 11, 203, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (99, 25, 7, 203, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (100, 30, 9, 203, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (101, 35, 12, 203, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (102, 5, 6, 204, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (103, 10, 8, 204, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (104, 15, 10, 204, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (105, 20, 11, 204, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (106, 25, 7, 204, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (107, 30, 9, 204, 0, N'')
INSERT [dbo].[Operation] ([id], [code], [type_id], [product_id], [coop_status_id], [description]) VALUES (108, 35, 12, 204, 0, N'')