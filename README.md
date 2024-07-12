Last updated: 20240711

# ExpenseTracker

[Chi_ExpenseTracker](https://chiexpensetracker.netlify.app/)

test account：Test@example.com

password：123456

# Introduction

這裡是介紹


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
