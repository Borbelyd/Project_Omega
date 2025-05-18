# Project Omega - Personal Finance Tracker

## Overview
  Try-live link:
  https://borbelyd.github.io/Project_Omega/

![image](https://github.com/user-attachments/assets/f29a1812-8d3c-4298-90ce-19b662d8cefa)

## Motivation

Project Omega is a personal finance tracking application designed to help users monitor their income and expenses efficiently. The motivation behind this project is to provide a simple, user-friendly interface for financial management without requiring complex setup or internet connectivity. By storing data locally in the browser, it ensures privacy while offering powerful visualization tools to understand spending habits.

## Features

- **Transaction Management**: Add income and expense transactions with detailed categorization
- **Data Visualization**: Interactive charts showing spending by category and monthly balance
- **Local Storage**: All data is stored locally in your browser for privacy
- **Dark/Light Theme**: Toggle between dark and light themes for comfortable viewing
- **CSV Export**: Export your financial data to CSV format for external analysis
- **Responsive Design**: Works on desktop and mobile devices

## Dependencies

### Backend
- .NET 7.0
- WebSharper (9.0.1.549)
- WebSharper.FSharp (9.0.1.549)
- WebSharper.UI (9.0.0.547)
- WebSharper.AspNetCore (9.0.1.549)

### Frontend
- TailwindCSS (via CDN)
- Chart.js (via CDN)
- ESBuild (0.19.9)

## Project Structure

```
Project_Omega/
├── Client.fs                  # F# client-side code (WebSharper)
├── Startup.fs                 # ASP.NET Core application startup
├── Project_Omega.fsproj       # F# project file
├── package.json               # NPM dependencies
├── esbuild.config.mjs         # ESBuild configuration
├── vite.config.js             # Vite configuration for development
├── wsconfig.json              # WebSharper configuration
├── Properties/                # ASP.NET Core properties
│   └── launchSettings.json    # Launch configuration
└── wwwroot/                   # Static web assets
    ├── index.html             # Main HTML template
    ├── CSS/                   # CSS styles
    │   └── style.css          # Custom styles
    └── Scripts/               # Generated JavaScript files
        ├── Project_Omega.js   # Main application script
        ├── Project_Omega.min.js # Minified application script
        └── ...
```

## How to Build and Run

### Prerequisites

1. [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
2. [Node.js](https://nodejs.org/) (for NPM and build tools)

### Building the Project

1. Clone the repository
   ```
   git clone https://github.com/borbelyd/Project_Omega.git
   cd Project_Omega
   ```

2. Install NPM dependencies
   ```
   npm install
   ```

3. Build the project
   ```
   dotnet build
   ```

### Running the Application

1. Start the application
   ```
   dotnet run
   ```

2. Open your browser and navigate to:
   ```
   https://localhost:5001
   ```
   or
   ```
   http://localhost:5000
   ```

## Usage

1. **Adding a Transaction**:
   - Fill in the amount, description, date, category, and transaction type
   - Click "Save" to record the transaction

2. **Viewing Statistics**:
   - Monthly breakdown chart shows your balance over time
   - Category distribution chart shows spending by category

3. **Exporting Data**:
   - Click "Export CSV" to download your transaction data in CSV format

4. **Switching Themes**:
   - Click "Toggle Theme" to switch between light and dark modes

## Contributing

Contributions to Project Omega are welcome! Here's how you can contribute:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

Please make sure to update tests as appropriate and adhere to the existing coding style.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Author

Created by Dominik Borbély
