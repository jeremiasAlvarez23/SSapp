USE [Alpine2]
GO
/****** Object:  Table [dbo].[AuditoriaMain]    Script Date: 17/6/2025 13:33:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditoriaMain](
	[AuditoriaId] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NULL,
	[Email] [nvarchar](100) NULL,
	[Nombre] [nvarchar](100) NULL,
	[Sistema] [nvarchar](100) NULL,
	[Accion] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](500) NULL,
	[Fecha] [datetime] NOT NULL,
	[Ip] [nvarchar](50) NULL,
	[Navegador] [nvarchar](200) NULL,
	[Exitoso] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AuditoriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AuditoriaMain] ON 

INSERT [dbo].[AuditoriaMain] ([AuditoriaId], [UsuarioId], [Email], [Nombre], [Sistema], [Accion], [Descripcion], [Fecha], [Ip], [Navegador], [Exitoso]) VALUES (1, -1, NULL, NULL, N'SecuritysApp', N'Alta Usuario', N'Se creó el usuario Puky.strazzolini@gmail.com | Entidad: Usuario (ID: 2) | Afectado UsuarioId: 2', CAST(N'2025-06-17T13:19:45.077' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36', 1)
INSERT [dbo].[AuditoriaMain] ([AuditoriaId], [UsuarioId], [Email], [Nombre], [Sistema], [Accion], [Descripcion], [Fecha], [Ip], [Navegador], [Exitoso]) VALUES (2, 2, NULL, NULL, N'SecuritysApp', N'Login Exitoso', N'Usuario Puky.strazzolini@gmail.com inició sesión correctamente | Entidad: Usuario (ID: 2) | Afectado UsuarioId: 2', CAST(N'2025-06-17T13:21:26.880' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36', 1)
INSERT [dbo].[AuditoriaMain] ([AuditoriaId], [UsuarioId], [Email], [Nombre], [Sistema], [Accion], [Descripcion], [Fecha], [Ip], [Navegador], [Exitoso]) VALUES (3, 2, NULL, NULL, N'SecuritysApp', N'Login Exitoso', N'Usuario Puky.strazzolini@gmail.com inició sesión correctamente | Entidad: Usuario (ID: 2) | Afectado UsuarioId: 2', CAST(N'2025-06-17T13:21:45.437' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36', 1)
INSERT [dbo].[AuditoriaMain] ([AuditoriaId], [UsuarioId], [Email], [Nombre], [Sistema], [Accion], [Descripcion], [Fecha], [Ip], [Navegador], [Exitoso]) VALUES (4, 2, NULL, NULL, N'SecuritysApp', N'Login Exitoso', N'Usuario Puky.strazzolini@gmail.com inició sesión correctamente | Entidad: Usuario (ID: 2) | Afectado UsuarioId: 2', CAST(N'2025-06-17T13:24:26.173' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36', 1)
INSERT [dbo].[AuditoriaMain] ([AuditoriaId], [UsuarioId], [Email], [Nombre], [Sistema], [Accion], [Descripcion], [Fecha], [Ip], [Navegador], [Exitoso]) VALUES (5, 2, NULL, NULL, N'SecuritysApp', N'Alta Sistema', N'Se creó el sistema SecuritysApp - Auditoria | Entidad: Sistema (ID: 1)', CAST(N'2025-06-17T13:27:47.083' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36', 1)
INSERT [dbo].[AuditoriaMain] ([AuditoriaId], [UsuarioId], [Email], [Nombre], [Sistema], [Accion], [Descripcion], [Fecha], [Ip], [Navegador], [Exitoso]) VALUES (6, 2, NULL, NULL, N'SecuritysApp', N'Editar Sistema', N'Sistema SecuritysApp - Auditoria editado | Entidad: Sistema (ID: 1)', CAST(N'2025-06-17T13:28:53.253' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36', 1)
INSERT [dbo].[AuditoriaMain] ([AuditoriaId], [UsuarioId], [Email], [Nombre], [Sistema], [Accion], [Descripcion], [Fecha], [Ip], [Navegador], [Exitoso]) VALUES (7, 2, NULL, NULL, N'SecuritysApp', N'Activar Sistema', N'Sistema SecuritysApp - Auditoria activado | Entidad: Sistema (ID: 1)', CAST(N'2025-06-17T13:29:01.237' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36', 1)
INSERT [dbo].[AuditoriaMain] ([AuditoriaId], [UsuarioId], [Email], [Nombre], [Sistema], [Accion], [Descripcion], [Fecha], [Ip], [Navegador], [Exitoso]) VALUES (8, 2, NULL, NULL, N'SecuritysApp', N'Desactivar Sistema', N'Sistema SecuritysApp - Auditoria desactivado | Entidad: Sistema (ID: 1)', CAST(N'2025-06-17T13:29:09.550' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36', 1)
INSERT [dbo].[AuditoriaMain] ([AuditoriaId], [UsuarioId], [Email], [Nombre], [Sistema], [Accion], [Descripcion], [Fecha], [Ip], [Navegador], [Exitoso]) VALUES (9, 2, NULL, NULL, N'SecuritysApp', N'Alta UsuarioSistema', N'Asignado sistema 1 al usuario 1 | Entidad: UsuarioSistema (ID: 1-1) | Afectado UsuarioId: 1', CAST(N'2025-06-17T13:30:09.783' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36', 1)
INSERT [dbo].[AuditoriaMain] ([AuditoriaId], [UsuarioId], [Email], [Nombre], [Sistema], [Accion], [Descripcion], [Fecha], [Ip], [Navegador], [Exitoso]) VALUES (10, 2, NULL, NULL, N'SecuritysApp', N'Activar Sistema', N'Sistema SecuritysApp - Auditoria activado | Entidad: Sistema (ID: 1)', CAST(N'2025-06-17T13:30:56.733' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36', 1)
INSERT [dbo].[AuditoriaMain] ([AuditoriaId], [UsuarioId], [Email], [Nombre], [Sistema], [Accion], [Descripcion], [Fecha], [Ip], [Navegador], [Exitoso]) VALUES (11, 2, NULL, NULL, N'SecuritysApp', N'Login Exitoso', N'Usuario Puky.strazzolini@gmail.com inició sesión correctamente | Entidad: Usuario (ID: 2) | Afectado UsuarioId: 2', CAST(N'2025-06-17T14:00:44.487' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36', 1)
SET IDENTITY_INSERT [dbo].[AuditoriaMain] OFF
GO
