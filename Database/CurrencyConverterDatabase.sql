Create Database CurrencyConverter

Use CurrencyConverter

CREATE TABLE Currencies (
    CurrencyCode CHAR(3) PRIMARY KEY,   -- e.g., USD, PKR, EUR
    CurrencyName NVARCHAR(100) NOT NULL, -- e.g., US Dollar, Pakistani Rupee
    Symbol NVARCHAR(10) NULL,           -- e.g., $, ?, €
    IsActive BIT DEFAULT 1
);

CREATE TABLE ExchangeRates (
    RateId INT PRIMARY KEY IDENTITY(1,1),
    BaseCurrency CHAR(3) NOT NULL,   -- e.g., USD
    TargetCurrency CHAR(3) NOT NULL, -- e.g., PKR
    Rate DECIMAL(18,6) NOT NULL,     -- conversion rate
    LastUpdated DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_ExchangeRates_Base FOREIGN KEY (BaseCurrency) REFERENCES Currencies(CurrencyCode),
    CONSTRAINT FK_ExchangeRates_Target FOREIGN KEY (TargetCurrency) REFERENCES Currencies(CurrencyCode)
);


CREATE TABLE ConversionHistory (
    HistoryId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NULL,   -- if you have users
    FromCurrency CHAR(3) NOT NULL,
    ToCurrency CHAR(3) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    ConvertedAmount DECIMAL(18,2) NOT NULL,
    RateUsed DECIMAL(18,6) NOT NULL,
    ConversionDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_ConversionHistory_From FOREIGN KEY (FromCurrency) REFERENCES Currencies(CurrencyCode),
    CONSTRAINT FK_ConversionHistory_To FOREIGN KEY (ToCurrency) REFERENCES Currencies(CurrencyCode)
);
