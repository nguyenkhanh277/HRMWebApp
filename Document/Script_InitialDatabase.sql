CREATE TABLE [dbo].[AuthorityGroups](
	[Id] [nvarchar](50) NOT NULL,
	[AuthorityGroupName] [nvarchar](200) NULL,
 CONSTRAINT [PK_dbo.AuthorityGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Company]    Script Date: 3/26/2021 12:54:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[ID] [nvarchar](50) NOT NULL,
	[CompanyName] [nvarchar](100) NULL,
	[Phone] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[TaxCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Payrolls]    Script Date: 3/26/2021 12:54:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payrolls](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [nvarchar](50) NULL,
	[MonthSalary] [int] NULL,
	[YearSalary] [int] NULL,
	[EmployeeCode] [nvarchar](50) NULL,
	[ActualWorkingDays] [float] NULL,
	[BasicSalary] [float] NULL,
	[HousingAlowance] [float] NULL,
	[TransportAllowance] [float] NULL,
	[MobileAllowance] [float] NULL,
	[MealAllowance] [float] NULL,
	[TotalSalaryAndAllowances] [float] NULL,
	[BonusPerformance] [float] NULL,
	[NontaxableIncome] [float] NULL,
	[CompanyBHXH] [float] NULL,
	[CompanyBHTNLD] [float] NULL,
	[CompanyBHYT] [float] NULL,
	[CompanyBHTN] [float] NULL,
	[CompanyTotal] [float] NULL,
	[CompanyKPCD] [float] NULL,
	[PersonalBHXH] [float] NULL,
	[PersonalBHYT] [float] NULL,
	[PersonalBHTN] [float] NULL,
	[PersonalTotal] [float] NULL,
	[PITPayable] [float] NULL,
	[SalaryPay] [float] NULL,
	[TaxableIncome] [float] NULL,
	[PersonalDeduction] [float] NULL,
	[NumberOfDependant] [int] NULL,
	[TotalDeductionForDependant] [float] NULL,
	[TotalEmployeeDeduction] [float] NULL,
	[IncomeToCalculatePITPayable] [float] NULL,
	[CalculatorPITPayable] [float] NULL,
 CONSTRAINT [PK_Payroll] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserAuthorities]    Script Date: 3/26/2021 12:54:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuthorities](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[AuthorityGroupId] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[EditedAt] [datetime] NULL,
	[EditedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.UserAuthorities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/26/2021 12:54:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Username] [nvarchar](50) NOT NULL,
	[CompanyID] [nvarchar](50) NOT NULL,
	[Salt] [nvarchar](50) NULL,
	[Password] [nvarchar](200) NULL,
	[EmployeeCode] [nvarchar](50) NULL,
	[EmployeeName] [nvarchar](50) NULL,
	[PersonalTaxCode] [nvarchar](50) NULL,
	[Position] [nvarchar](50) NULL,
	[Department] [nvarchar](50) NULL,
	[StartDate] [datetime] NULL,
	[TypeOfContact] [nvarchar](50) NULL,
	[Status] [int] NOT NULL CONSTRAINT [DF_Users_Status]  DEFAULT ((1)),
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[EditedAt] [datetime] NULL,
	[EditedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Username] ASC,
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[AuthorityGroups] ([Id], [AuthorityGroupName]) VALUES (N'Admin', N'Quyền quản trị')
INSERT [dbo].[AuthorityGroups] ([Id], [AuthorityGroupName]) VALUES (N'CreateUser', N'Quyền tạo tài khoản')
INSERT [dbo].[AuthorityGroups] ([Id], [AuthorityGroupName]) VALUES (N'UploadPayrolls', N'Quyền tải lên bảng lương')
INSERT [dbo].[AuthorityGroups] ([Id], [AuthorityGroupName]) VALUES (N'ViewPayrolls', N'Quyền xem bảng lương')
INSERT [dbo].[Company] ([ID], [CompanyName], [Phone], [Address], [TaxCode]) VALUES (N'1', N'DataTech', N'', N'', N'')
SET IDENTITY_INSERT [dbo].[Payrolls] ON 

INSERT [dbo].[Payrolls] ([ID], [CompanyID], [MonthSalary], [YearSalary], [EmployeeCode], [ActualWorkingDays], [BasicSalary], [HousingAlowance], [TransportAllowance], [MobileAllowance], [MealAllowance], [TotalSalaryAndAllowances], [BonusPerformance], [NontaxableIncome], [CompanyBHXH], [CompanyBHTNLD], [CompanyBHYT], [CompanyBHTN], [CompanyTotal], [CompanyKPCD], [PersonalBHXH], [PersonalBHYT], [PersonalBHTN], [PersonalTotal], [PITPayable], [SalaryPay], [TaxableIncome], [PersonalDeduction], [NumberOfDependant], [TotalDeductionForDependant], [TotalEmployeeDeduction], [IncomeToCalculatePITPayable], [CalculatorPITPayable]) VALUES (21, N'1', 3, 2021, N'NV0001', 20, 7770000, 1000000, 1000000, 1000000, 730000, 11500000, 0, 1730000, 1320900, 38850, 233100, 77700, 1670550, 155400, 621600, 116550, 77700, 815850, 0, 10684150, 9770000, 11000000, 1, 4400000, 15400000, 0, 0)
SET IDENTITY_INSERT [dbo].[Payrolls] OFF
SET IDENTITY_INSERT [dbo].[UserAuthorities] ON 

INSERT [dbo].[UserAuthorities] ([Id], [Username], [AuthorityGroupId], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (1, N'administrator', N'Admin', CAST(N'2021-03-01 00:00:00.000' AS DateTime), N'Administrator', NULL, NULL)
INSERT [dbo].[UserAuthorities] ([Id], [Username], [AuthorityGroupId], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (2, N'khanh', N'CreateUser', CAST(N'2021-03-01 00:00:00.000' AS DateTime), N'Administrator', NULL, NULL)
INSERT [dbo].[UserAuthorities] ([Id], [Username], [AuthorityGroupId], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (6, N'khanh', N'UploadPayrolls', CAST(N'2021-03-01 00:00:00.000' AS DateTime), N'Administrator', NULL, NULL)
INSERT [dbo].[UserAuthorities] ([Id], [Username], [AuthorityGroupId], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (7, N'khanh', N'ViewPayrolls', CAST(N'2021-03-01 00:00:00.000' AS DateTime), N'Administrator', NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserAuthorities] OFF
INSERT [dbo].[Users] ([Username], [CompanyID], [Salt], [Password], [EmployeeCode], [EmployeeName], [PersonalTaxCode], [Position], [Department], [StartDate], [TypeOfContact], [Status], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (N'administrator', N'1', N'taUBl', N'cd1ed6520e68b8921b318cad5a9c0c81', NULL, N'Administrator', NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2021-03-01 00:00:00.000' AS DateTime), N'Administrator', NULL, NULL)
INSERT [dbo].[Users] ([Username], [CompanyID], [Salt], [Password], [EmployeeCode], [EmployeeName], [PersonalTaxCode], [Position], [Department], [StartDate], [TypeOfContact], [Status], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (N'khanh', N'1', N'taUBl', N'784d77d278cbe2bed8265332acf3662f', N'NV0001', N'Nguyễn Xuân Khánh', N'', N'', N'', CAST(N'2019-03-23 00:00:00.000' AS DateTime), N'', 1, CAST(N'2021-03-01 00:00:00.000' AS DateTime), N'Administrator', NULL, NULL)
ALTER TABLE [dbo].[UserAuthorities]  WITH CHECK ADD  CONSTRAINT [FK_UserAuthorities_AuthorityGroups] FOREIGN KEY([AuthorityGroupId])
REFERENCES [dbo].[AuthorityGroups] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserAuthorities] CHECK CONSTRAINT [FK_UserAuthorities_AuthorityGroups]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Company] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Company]
GO
