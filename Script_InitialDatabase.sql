
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuthorityGroups](
	[Id] [nvarchar](50) NOT NULL,
	[AuthorityGroupName] [nvarchar](200) NULL,
 CONSTRAINT [PK_dbo.AuthorityGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 23/04/2021 11:17:32 SA ******/
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
/****** Object:  Table [dbo].[Payrolls]    Script Date: 23/04/2021 11:17:32 SA ******/
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
	[BasicSalary] [nvarchar](200) NULL,
	[HousingAlowance] [nvarchar](200) NULL,
	[TransportAllowance] [nvarchar](200) NULL,
	[MobileAllowance] [nvarchar](200) NULL,
	[MealAllowance] [nvarchar](200) NULL,
	[TotalSalaryAndAllowances] [nvarchar](200) NULL,
	[BonusPerformance] [nvarchar](200) NULL,
	[NontaxableIncome] [nvarchar](200) NULL,
	[CompanyBHXH] [nvarchar](200) NULL,
	[CompanyBHTNLD] [nvarchar](200) NULL,
	[CompanyBHYT] [nvarchar](200) NULL,
	[CompanyBHTN] [nvarchar](200) NULL,
	[CompanyTotal] [nvarchar](200) NULL,
	[CompanyKPCD] [nvarchar](200) NULL,
	[PersonalBHXH] [nvarchar](200) NULL,
	[PersonalBHYT] [nvarchar](200) NULL,
	[PersonalBHTN] [nvarchar](200) NULL,
	[PersonalTotal] [nvarchar](200) NULL,
	[PITPayable] [nvarchar](200) NULL,
	[SalaryPay] [nvarchar](200) NULL,
	[TaxableIncome] [nvarchar](200) NULL,
	[PersonalDeduction] [nvarchar](200) NULL,
	[NumberOfDependant] [nvarchar](200) NULL,
	[TotalDeductionForDependant] [nvarchar](200) NULL,
	[TotalEmployeeDeduction] [nvarchar](200) NULL,
	[IncomeToCalculatePITPayable] [nvarchar](200) NULL,
	[CalculatorPITPayable] [nvarchar](200) NULL,
 CONSTRAINT [PK_Payroll] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAuthorities]    Script Date: 23/04/2021 11:17:32 SA ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 23/04/2021 11:17:32 SA ******/
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
	[Status] [int] NOT NULL,
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
GO
INSERT [dbo].[Company] ([ID], [CompanyName], [Phone], [Address], [TaxCode]) VALUES (N'1', N'DataTech', N'', N'', N'')
GO
SET IDENTITY_INSERT [dbo].[Payrolls] ON 

INSERT [dbo].[Payrolls] ([ID], [CompanyID], [MonthSalary], [YearSalary], [EmployeeCode], [ActualWorkingDays], [BasicSalary], [HousingAlowance], [TransportAllowance], [MobileAllowance], [MealAllowance], [TotalSalaryAndAllowances], [BonusPerformance], [NontaxableIncome], [CompanyBHXH], [CompanyBHTNLD], [CompanyBHYT], [CompanyBHTN], [CompanyTotal], [CompanyKPCD], [PersonalBHXH], [PersonalBHYT], [PersonalBHTN], [PersonalTotal], [PITPayable], [SalaryPay], [TaxableIncome], [PersonalDeduction], [NumberOfDependant], [TotalDeductionForDependant], [TotalEmployeeDeduction], [IncomeToCalculatePITPayable], [CalculatorPITPayable]) VALUES (28, N'1', 3, 2021, N'NV0001', 20, N'oUUFHDKTM+w=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'GycogMbVxI0=', N'av4FKij33HifcUG72hWitw==', N'teKC0WZS5Dc=', N'l/LRLVjjr+c=', N'DjnRgBhHZGc=', N'ubgI8MGrqyc=', N'ZPggX78GB4M=', N'cwcPnSXkJn0=', N'szDSWmAJUlc=', N'YCWnF54hhqg=', N'1GoavBEJyls=', N'+b3ub0mz7UQ=', N'cwcPnSXkJn0=', N'JuTNMNMq5+E=', N'teKC0WZS5Dc=', N'a2m8NKom+vifcUG72hWitw==', N'w4CujobIYTs=', N'BD5cOkkvrxafcUG72hWitw==', N'+g1FsV3ZDXY=', N'hxEWFUpo8ms=', N'kyC+C7QjeSKfcUG72hWitw==', N'teKC0WZS5Dc=', N'teKC0WZS5Dc=')
INSERT [dbo].[Payrolls] ([ID], [CompanyID], [MonthSalary], [YearSalary], [EmployeeCode], [ActualWorkingDays], [BasicSalary], [HousingAlowance], [TransportAllowance], [MobileAllowance], [MealAllowance], [TotalSalaryAndAllowances], [BonusPerformance], [NontaxableIncome], [CompanyBHXH], [CompanyBHTNLD], [CompanyBHYT], [CompanyBHTN], [CompanyTotal], [CompanyKPCD], [PersonalBHXH], [PersonalBHYT], [PersonalBHTN], [PersonalTotal], [PITPayable], [SalaryPay], [TaxableIncome], [PersonalDeduction], [NumberOfDependant], [TotalDeductionForDependant], [TotalEmployeeDeduction], [IncomeToCalculatePITPayable], [CalculatorPITPayable]) VALUES (29, N'1', 3, 2021, N'NV0002', 19, N'4mjJyOLb9NM=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'GycogMbVxI0=', N'mKWLuBuJcCOfcUG72hWitw==', N'teKC0WZS5Dc=', N'l/LRLVjjr+c=', N'GhZ8ppqAtSI=', N'A14w3OngHgk=', N'NhyXjTRWF/E=', N'CU1Q1+XLYEc=', N'HON4jeuuHMY=', N'5YVD0SnqLRI=', N'EYUv+O8YLlg=', N'6ribt8fvLX4=', N'CU1Q1+XLYEc=', N'/ro0QLirUFk=', N'teKC0WZS5Dc=', N'bRsKUyI4hJmfcUG72hWitw==', N'tfClSPyzgQGfcUG72hWitw==', N'BD5cOkkvrxafcUG72hWitw==', N'+g1FsV3ZDXY=', N'hxEWFUpo8ms=', N'kyC+C7QjeSKfcUG72hWitw==', N'teKC0WZS5Dc=', N'teKC0WZS5Dc=')
INSERT [dbo].[Payrolls] ([ID], [CompanyID], [MonthSalary], [YearSalary], [EmployeeCode], [ActualWorkingDays], [BasicSalary], [HousingAlowance], [TransportAllowance], [MobileAllowance], [MealAllowance], [TotalSalaryAndAllowances], [BonusPerformance], [NontaxableIncome], [CompanyBHXH], [CompanyBHTNLD], [CompanyBHYT], [CompanyBHTN], [CompanyTotal], [CompanyKPCD], [PersonalBHXH], [PersonalBHYT], [PersonalBHTN], [PersonalTotal], [PITPayable], [SalaryPay], [TaxableIncome], [PersonalDeduction], [NumberOfDependant], [TotalDeductionForDependant], [TotalEmployeeDeduction], [IncomeToCalculatePITPayable], [CalculatorPITPayable]) VALUES (30, N'1', 3, 2021, N'NV0003', 21, N'pSKvts/aF+g=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'GycogMbVxI0=', N'bhmOC66kGqefcUG72hWitw==', N'teKC0WZS5Dc=', N'l/LRLVjjr+c=', N'TS3SN78I7oI=', N'kfU3OCEzo9g=', N'OuavVo84X1w=', N'GBgbI7guZkM=', N'7DjI3a/XdYY=', N'lZrGL0XH28U=', N'CtdQAFevk2A=', N'touRD8m7msk=', N'GBgbI7guZkM=', N'XUkf0qIBz00=', N'teKC0WZS5Dc=', N'yyY12RW2JTefcUG72hWitw==', N'BD5cOkkvrxafcUG72hWitw==', N'BD5cOkkvrxafcUG72hWitw==', N'+g1FsV3ZDXY=', N'hxEWFUpo8ms=', N'kyC+C7QjeSKfcUG72hWitw==', N'teKC0WZS5Dc=', N'teKC0WZS5Dc=')
INSERT [dbo].[Payrolls] ([ID], [CompanyID], [MonthSalary], [YearSalary], [EmployeeCode], [ActualWorkingDays], [BasicSalary], [HousingAlowance], [TransportAllowance], [MobileAllowance], [MealAllowance], [TotalSalaryAndAllowances], [BonusPerformance], [NontaxableIncome], [CompanyBHXH], [CompanyBHTNLD], [CompanyBHYT], [CompanyBHTN], [CompanyTotal], [CompanyKPCD], [PersonalBHXH], [PersonalBHYT], [PersonalBHTN], [PersonalTotal], [PITPayable], [SalaryPay], [TaxableIncome], [PersonalDeduction], [NumberOfDependant], [TotalDeductionForDependant], [TotalEmployeeDeduction], [IncomeToCalculatePITPayable], [CalculatorPITPayable]) VALUES (31, N'1', 1, 2021, N'NV0001', 20, N'oUUFHDKTM+w=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'GycogMbVxI0=', N'av4FKij33HifcUG72hWitw==', N'teKC0WZS5Dc=', N'l/LRLVjjr+c=', N'DjnRgBhHZGc=', N'ubgI8MGrqyc=', N'ZPggX78GB4M=', N'cwcPnSXkJn0=', N'szDSWmAJUlc=', N'YCWnF54hhqg=', N'1GoavBEJyls=', N'+b3ub0mz7UQ=', N'cwcPnSXkJn0=', N'JuTNMNMq5+E=', N'teKC0WZS5Dc=', N'a2m8NKom+vifcUG72hWitw==', N'w4CujobIYTs=', N'BD5cOkkvrxafcUG72hWitw==', N'+g1FsV3ZDXY=', N'hxEWFUpo8ms=', N'kyC+C7QjeSKfcUG72hWitw==', N'teKC0WZS5Dc=', N'teKC0WZS5Dc=')
INSERT [dbo].[Payrolls] ([ID], [CompanyID], [MonthSalary], [YearSalary], [EmployeeCode], [ActualWorkingDays], [BasicSalary], [HousingAlowance], [TransportAllowance], [MobileAllowance], [MealAllowance], [TotalSalaryAndAllowances], [BonusPerformance], [NontaxableIncome], [CompanyBHXH], [CompanyBHTNLD], [CompanyBHYT], [CompanyBHTN], [CompanyTotal], [CompanyKPCD], [PersonalBHXH], [PersonalBHYT], [PersonalBHTN], [PersonalTotal], [PITPayable], [SalaryPay], [TaxableIncome], [PersonalDeduction], [NumberOfDependant], [TotalDeductionForDependant], [TotalEmployeeDeduction], [IncomeToCalculatePITPayable], [CalculatorPITPayable]) VALUES (32, N'1', 1, 2021, N'NV0002', 19, N'4mjJyOLb9NM=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'GycogMbVxI0=', N'mKWLuBuJcCOfcUG72hWitw==', N'teKC0WZS5Dc=', N'l/LRLVjjr+c=', N'GhZ8ppqAtSI=', N'A14w3OngHgk=', N'NhyXjTRWF/E=', N'CU1Q1+XLYEc=', N'HON4jeuuHMY=', N'5YVD0SnqLRI=', N'EYUv+O8YLlg=', N'6ribt8fvLX4=', N'CU1Q1+XLYEc=', N'/ro0QLirUFk=', N'teKC0WZS5Dc=', N'bRsKUyI4hJmfcUG72hWitw==', N'tfClSPyzgQGfcUG72hWitw==', N'BD5cOkkvrxafcUG72hWitw==', N'+g1FsV3ZDXY=', N'hxEWFUpo8ms=', N'kyC+C7QjeSKfcUG72hWitw==', N'teKC0WZS5Dc=', N'teKC0WZS5Dc=')
INSERT [dbo].[Payrolls] ([ID], [CompanyID], [MonthSalary], [YearSalary], [EmployeeCode], [ActualWorkingDays], [BasicSalary], [HousingAlowance], [TransportAllowance], [MobileAllowance], [MealAllowance], [TotalSalaryAndAllowances], [BonusPerformance], [NontaxableIncome], [CompanyBHXH], [CompanyBHTNLD], [CompanyBHYT], [CompanyBHTN], [CompanyTotal], [CompanyKPCD], [PersonalBHXH], [PersonalBHYT], [PersonalBHTN], [PersonalTotal], [PITPayable], [SalaryPay], [TaxableIncome], [PersonalDeduction], [NumberOfDependant], [TotalDeductionForDependant], [TotalEmployeeDeduction], [IncomeToCalculatePITPayable], [CalculatorPITPayable]) VALUES (33, N'1', 1, 2021, N'NV0003', 21, N'pSKvts/aF+g=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'GycogMbVxI0=', N'bhmOC66kGqefcUG72hWitw==', N'teKC0WZS5Dc=', N'l/LRLVjjr+c=', N'TS3SN78I7oI=', N'kfU3OCEzo9g=', N'OuavVo84X1w=', N'GBgbI7guZkM=', N'7DjI3a/XdYY=', N'lZrGL0XH28U=', N'CtdQAFevk2A=', N'touRD8m7msk=', N'GBgbI7guZkM=', N'XUkf0qIBz00=', N'teKC0WZS5Dc=', N'yyY12RW2JTefcUG72hWitw==', N'BD5cOkkvrxafcUG72hWitw==', N'BD5cOkkvrxafcUG72hWitw==', N'+g1FsV3ZDXY=', N'hxEWFUpo8ms=', N'kyC+C7QjeSKfcUG72hWitw==', N'teKC0WZS5Dc=', N'teKC0WZS5Dc=')
INSERT [dbo].[Payrolls] ([ID], [CompanyID], [MonthSalary], [YearSalary], [EmployeeCode], [ActualWorkingDays], [BasicSalary], [HousingAlowance], [TransportAllowance], [MobileAllowance], [MealAllowance], [TotalSalaryAndAllowances], [BonusPerformance], [NontaxableIncome], [CompanyBHXH], [CompanyBHTNLD], [CompanyBHYT], [CompanyBHTN], [CompanyTotal], [CompanyKPCD], [PersonalBHXH], [PersonalBHYT], [PersonalBHTN], [PersonalTotal], [PITPayable], [SalaryPay], [TaxableIncome], [PersonalDeduction], [NumberOfDependant], [TotalDeductionForDependant], [TotalEmployeeDeduction], [IncomeToCalculatePITPayable], [CalculatorPITPayable]) VALUES (34, N'1', 4, 2021, N'NV0001', 20, N'oUUFHDKTM+w=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'GycogMbVxI0=', N'av4FKij33HifcUG72hWitw==', N'teKC0WZS5Dc=', N'l/LRLVjjr+c=', N'DjnRgBhHZGc=', N'ubgI8MGrqyc=', N'ZPggX78GB4M=', N'cwcPnSXkJn0=', N'szDSWmAJUlc=', N'YCWnF54hhqg=', N'1GoavBEJyls=', N'+b3ub0mz7UQ=', N'cwcPnSXkJn0=', N'JuTNMNMq5+E=', N'teKC0WZS5Dc=', N'a2m8NKom+vifcUG72hWitw==', N'w4CujobIYTs=', N'BD5cOkkvrxafcUG72hWitw==', N'+g1FsV3ZDXY=', N'hxEWFUpo8ms=', N'kyC+C7QjeSKfcUG72hWitw==', N'teKC0WZS5Dc=', N'teKC0WZS5Dc=')
INSERT [dbo].[Payrolls] ([ID], [CompanyID], [MonthSalary], [YearSalary], [EmployeeCode], [ActualWorkingDays], [BasicSalary], [HousingAlowance], [TransportAllowance], [MobileAllowance], [MealAllowance], [TotalSalaryAndAllowances], [BonusPerformance], [NontaxableIncome], [CompanyBHXH], [CompanyBHTNLD], [CompanyBHYT], [CompanyBHTN], [CompanyTotal], [CompanyKPCD], [PersonalBHXH], [PersonalBHYT], [PersonalBHTN], [PersonalTotal], [PITPayable], [SalaryPay], [TaxableIncome], [PersonalDeduction], [NumberOfDependant], [TotalDeductionForDependant], [TotalEmployeeDeduction], [IncomeToCalculatePITPayable], [CalculatorPITPayable]) VALUES (35, N'1', 4, 2021, N'NV0002', 19, N'4mjJyOLb9NM=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'GycogMbVxI0=', N'mKWLuBuJcCOfcUG72hWitw==', N'teKC0WZS5Dc=', N'l/LRLVjjr+c=', N'GhZ8ppqAtSI=', N'A14w3OngHgk=', N'NhyXjTRWF/E=', N'CU1Q1+XLYEc=', N'HON4jeuuHMY=', N'5YVD0SnqLRI=', N'EYUv+O8YLlg=', N'6ribt8fvLX4=', N'CU1Q1+XLYEc=', N'/ro0QLirUFk=', N'teKC0WZS5Dc=', N'bRsKUyI4hJmfcUG72hWitw==', N'tfClSPyzgQGfcUG72hWitw==', N'BD5cOkkvrxafcUG72hWitw==', N'+g1FsV3ZDXY=', N'hxEWFUpo8ms=', N'kyC+C7QjeSKfcUG72hWitw==', N'teKC0WZS5Dc=', N'teKC0WZS5Dc=')
INSERT [dbo].[Payrolls] ([ID], [CompanyID], [MonthSalary], [YearSalary], [EmployeeCode], [ActualWorkingDays], [BasicSalary], [HousingAlowance], [TransportAllowance], [MobileAllowance], [MealAllowance], [TotalSalaryAndAllowances], [BonusPerformance], [NontaxableIncome], [CompanyBHXH], [CompanyBHTNLD], [CompanyBHYT], [CompanyBHTN], [CompanyTotal], [CompanyKPCD], [PersonalBHXH], [PersonalBHYT], [PersonalBHTN], [PersonalTotal], [PITPayable], [SalaryPay], [TaxableIncome], [PersonalDeduction], [NumberOfDependant], [TotalDeductionForDependant], [TotalEmployeeDeduction], [IncomeToCalculatePITPayable], [CalculatorPITPayable]) VALUES (36, N'1', 4, 2021, N'NV0003', 21, N'pSKvts/aF+g=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'2Gwp2xtlSgw=', N'GycogMbVxI0=', N'bhmOC66kGqefcUG72hWitw==', N'teKC0WZS5Dc=', N'l/LRLVjjr+c=', N'TS3SN78I7oI=', N'kfU3OCEzo9g=', N'OuavVo84X1w=', N'GBgbI7guZkM=', N'7DjI3a/XdYY=', N'lZrGL0XH28U=', N'CtdQAFevk2A=', N'touRD8m7msk=', N'GBgbI7guZkM=', N'XUkf0qIBz00=', N'teKC0WZS5Dc=', N'yyY12RW2JTefcUG72hWitw==', N'BD5cOkkvrxafcUG72hWitw==', N'BD5cOkkvrxafcUG72hWitw==', N'+g1FsV3ZDXY=', N'hxEWFUpo8ms=', N'kyC+C7QjeSKfcUG72hWitw==', N'teKC0WZS5Dc=', N'teKC0WZS5Dc=')
SET IDENTITY_INSERT [dbo].[Payrolls] OFF
GO
SET IDENTITY_INSERT [dbo].[UserAuthorities] ON 

INSERT [dbo].[UserAuthorities] ([Id], [Username], [AuthorityGroupId], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (1, N'administrator', N'Admin', CAST(N'2021-03-01T00:00:00.000' AS DateTime), N'Administrator', NULL, NULL)
INSERT [dbo].[UserAuthorities] ([Id], [Username], [AuthorityGroupId], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (2, N'khanh', N'CreateUser', CAST(N'2021-03-01T00:00:00.000' AS DateTime), N'Administrator', NULL, NULL)
INSERT [dbo].[UserAuthorities] ([Id], [Username], [AuthorityGroupId], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (6, N'khanh', N'UploadPayrolls', CAST(N'2021-03-01T00:00:00.000' AS DateTime), N'Administrator', NULL, NULL)
INSERT [dbo].[UserAuthorities] ([Id], [Username], [AuthorityGroupId], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (7, N'khanh', N'ViewPayrolls', CAST(N'2021-03-01T00:00:00.000' AS DateTime), N'Administrator', NULL, NULL)
INSERT [dbo].[UserAuthorities] ([Id], [Username], [AuthorityGroupId], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (20, N'acc', N'CreateUser', CAST(N'2021-04-22T09:04:55.000' AS DateTime), N'administrator', NULL, NULL)
INSERT [dbo].[UserAuthorities] ([Id], [Username], [AuthorityGroupId], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (21, N'acc', N'UploadPayrolls', CAST(N'2021-04-22T09:04:55.000' AS DateTime), N'administrator', NULL, NULL)
INSERT [dbo].[UserAuthorities] ([Id], [Username], [AuthorityGroupId], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (22, N'acc', N'ViewPayrolls', CAST(N'2021-04-22T09:04:55.000' AS DateTime), N'administrator', NULL, NULL)
INSERT [dbo].[UserAuthorities] ([Id], [Username], [AuthorityGroupId], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (24, N'admin', N'Admin', CAST(N'2021-04-22T10:23:28.000' AS DateTime), N'administrator', NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserAuthorities] OFF
GO
INSERT [dbo].[Users] ([Username], [CompanyID], [Salt], [Password], [EmployeeCode], [EmployeeName], [PersonalTaxCode], [Position], [Department], [StartDate], [TypeOfContact], [Status], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (N'acc', N'1', N'yVbv1', N'f13c6d62ddf1c39e4b7114d1ef758fd0', N'Acc', N'Kế toán', N'', N'', N'', CAST(N'2021-04-22T00:00:00.000' AS DateTime), N'', 1, CAST(N'2021-04-22T09:04:15.000' AS DateTime), N'administrator', NULL, NULL)
INSERT [dbo].[Users] ([Username], [CompanyID], [Salt], [Password], [EmployeeCode], [EmployeeName], [PersonalTaxCode], [Position], [Department], [StartDate], [TypeOfContact], [Status], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (N'admin', N'1', N'mvTOg', N'0e14b47a529f6a499ac4f93f7cd616a5', N'003', N'admin', N'', N'', N'', CAST(N'2021-04-22T00:00:00.000' AS DateTime), N'', 1, CAST(N'2021-04-22T10:21:52.000' AS DateTime), N'administrator', NULL, NULL)
INSERT [dbo].[Users] ([Username], [CompanyID], [Salt], [Password], [EmployeeCode], [EmployeeName], [PersonalTaxCode], [Position], [Department], [StartDate], [TypeOfContact], [Status], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (N'administrator', N'1', N'taUBl', N'f41e27de830e20d609a14d6b521d5db8', NULL, N'Administrator', NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2021-03-01T00:00:00.000' AS DateTime), N'Administrator', NULL, NULL)
INSERT [dbo].[Users] ([Username], [CompanyID], [Salt], [Password], [EmployeeCode], [EmployeeName], [PersonalTaxCode], [Position], [Department], [StartDate], [TypeOfContact], [Status], [CreatedAt], [CreatedBy], [EditedAt], [EditedBy]) VALUES (N'demo', N'1', N'r1YEe', N'2d37d4a708ec22e82abf13309ae3f96a', N'Demo', N'Demo', N'', N'', N'', CAST(N'2021-04-19T00:00:00.000' AS DateTime), N'', 1, CAST(N'2021-04-19T10:57:09.000' AS DateTime), N'administrator', NULL, NULL)
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Status]  DEFAULT ((1)) FOR [Status]
GO
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
