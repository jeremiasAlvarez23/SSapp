USE [Alpine]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 17/6/2025 13:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Componente] [nvarchar](100) NULL,
	[Ruta] [nvarchar](255) NULL,
	[MenuPadreId] [int] NOT NULL,
	[Orden] [int] NULL,
	[Icono] [nvarchar](100) NULL,
	[Color] [nvarchar](50) NULL,
	[Visible] [bit] NULL,
	[Activo] [bit] NULL,
	[SistemaId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 17/6/2025 13:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[RolId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[RolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolMenu]    Script Date: 17/6/2025 13:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RolId] [int] NULL,
	[MenuId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sistema]    Script Date: 17/6/2025 13:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sistema](
	[SistemaId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[UrlBase] [nvarchar](255) NOT NULL,
	[Descripcion] [nvarchar](255) NULL,
	[Activo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SistemaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 17/6/2025 13:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[Activo] [bit] NOT NULL,
	[RolId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuarioSistema]    Script Date: 17/6/2025 13:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioSistema](
	[UsuarioId] [int] NOT NULL,
	[SistemaId] [int] NOT NULL,
	[RolSistema] [nvarchar](100) NOT NULL,
	[TieneAcceso] [bit] NOT NULL,
 CONSTRAINT [PK_UsuarioSistema] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC,
	[SistemaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([RolId], [Nombre], [Descripcion]) VALUES (1, N'Administrador', N'Administrador Principal')
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[Sistema] ON 

INSERT [dbo].[Sistema] ([SistemaId], [Nombre], [UrlBase], [Descripcion], [Activo]) VALUES (1, N'SecuritysApp - Auditoria', N'SecuritysApp/AuditoriaPrincipal', N'Auditoria Principal', 1)
SET IDENTITY_INSERT [dbo].[Sistema] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([UsuarioId], [Nombre], [Email], [PasswordHash], [Activo], [RolId]) VALUES (1, N'Marcos', N'Marcos.strazzolini@gmail.com', N'123123', 1, 1)
INSERT [dbo].[Usuario] ([UsuarioId], [Nombre], [Email], [PasswordHash], [Activo], [RolId]) VALUES (2, N'Puky', N'Puky.strazzolini@gmail.com', N'$2a$11$NSV//kwPqfyRabCvQ4mPS.2ha49dUQR6libOFcF9KmTalfsD0pxR.', 1, 1)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
INSERT [dbo].[UsuarioSistema] ([UsuarioId], [SistemaId], [RolSistema], [TieneAcceso]) VALUES (1, 1, N'1', 1)
GO
ALTER TABLE [dbo].[Menu] ADD  DEFAULT ((0)) FOR [MenuPadreId]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Sistema] FOREIGN KEY([SistemaId])
REFERENCES [dbo].[Sistema] ([SistemaId])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_Sistema]
GO
ALTER TABLE [dbo].[RolMenu]  WITH CHECK ADD  CONSTRAINT [FK_RolMenu_Menu] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Menu] ([MenuId])
GO
ALTER TABLE [dbo].[RolMenu] CHECK CONSTRAINT [FK_RolMenu_Menu]
GO
ALTER TABLE [dbo].[RolMenu]  WITH CHECK ADD  CONSTRAINT [FK_RolMenu_Rol] FOREIGN KEY([RolId])
REFERENCES [dbo].[Rol] ([RolId])
GO
ALTER TABLE [dbo].[RolMenu] CHECK CONSTRAINT [FK_RolMenu_Rol]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([RolId])
REFERENCES [dbo].[Rol] ([RolId])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
ALTER TABLE [dbo].[UsuarioSistema]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioSistema_Sistema] FOREIGN KEY([SistemaId])
REFERENCES [dbo].[Sistema] ([SistemaId])
GO
ALTER TABLE [dbo].[UsuarioSistema] CHECK CONSTRAINT [FK_UsuarioSistema_Sistema]
GO
ALTER TABLE [dbo].[UsuarioSistema]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioSistema_Usuario] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuario] ([UsuarioId])
GO
ALTER TABLE [dbo].[UsuarioSistema] CHECK CONSTRAINT [FK_UsuarioSistema_Usuario]
GO
