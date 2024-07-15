Last updated: 20240711

# ExpenseTracker

[Chi_ExpenseTracker](https://chiexpensetracker.netlify.app/)

Test account：Test@example.com

password：123456

# Introduction

### [簡介]

Chi_ExpenseTracker 是一個專為個人設計的簡單記帳系統，

希望幫助用戶輕鬆追蹤和管理日常財務活動。

### [主要功能]

總覽頁：日期區間的總計金額與報表，快速了解近期收支狀況。

交易頁：支出和收入記錄，支持多種分類。

類別頁：可設定支出與收入的分類，方便用戶管理。

### [技術簡介]

Chi_ExpenseTracker 以 .NET 8.0 為核心技術，

採用 Entity Framework Core 和 Dapper 進行資料存取，

並使用 Microsoft SQL Server 作為後端資料庫。

系統架構上，採用分層設計，將應用程式分為 WebAPI 接口層、服務邏輯層和資料存取層，

並利用 Autofac 進行依賴注入，確保代碼的可維護性和可擴展性。

# Tech Stack

### Core

* ASP.NET Core Web API (.NET 8.0)

### ORM

* Entity Framework Core 8.0.6 (DB First)
* Dapper 2.1.35

### DB

* Microsoft SQL Server

### Tool

* EF Core T4 CodeTemplate
* IOC：Autofac DependencyInjection 9.0.0
* Auth：JWT Authentication 8.0.6
* Encryption：BCrypt.Net 4.0.3

# Information

### WepApi接口(Chi_ExpenseTracker_WebApi)

    Api接口 : Controllers\
    
    appsettings.json : 參數設定檔(預設)
        appsettings.Development.json : 本機參數設定檔(環境變數ASPNETCORE_ENVIRONMENT=Development)
    
    Program.cs : 程式啟動進入點、服務註冊

### 服務邏輯層(Chi_ExpenseTracker_Service)

    邏輯服務、服務接口： Common\
    
    傳入與傳出模型： Models\
    
    API統一回傳模型：Models\Api
    
    Api方法Enums : Models\Api\Enum\ApiCodeEnum.cs

### 資料存取層(Chi_ExpenseTracker_Repesitory)

    資料庫讀取(擴充方法) : Database\DbBase.cs
    
    資料存取 : Database\Repository
    
    資料庫表格Entity : Models\
    
    參數設定檔模型 : Configuration\AppSettings.cs
    
    架構基礎設定：Infrastructure\
    
    Jwt驗證：Infrastructure\Jwt
    
    IOC注入設定 : Infrastructure\Ioc\AutofacModuleRegister

# Future

* 分帳功能：多用戶共同管理同一個收支專案，實現分帳功能。
* 預算管理：幫助用戶制定和追蹤預算，避免超支。
* 報告分析：生成詳細的年、月、日財務報告，分析消費習慣，幫助用戶更好地管理財務。
* 報表下載：讓用戶可以將財務數據以Excel格式下載，以便進行進一步的分析和存檔。
