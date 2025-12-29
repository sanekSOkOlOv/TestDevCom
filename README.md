# TestDevCom

A brief README for the TestDevCom project describing deployment on Azure: API in App Service and frontend (MVC) in a separate container.

## Overview
TestDevCom is a web application with server logic in C# (.NET) and frontend in MVC (HTML/CSS, minimal JS). When deployed on Azure, the architecture is divided into two independent components:

- API (business logic processing and database operations) — deployed in Azure App Service (Windows/Linux App Service).
- Frontend (MVC) — packaged in a Docker container and deployed separately in Azure (Container Instance or App Service for Containers). The frontend accesses the API to work with the database via a public/internal API URL.

Language composition of the repository:
- C# — 55.5%
- HTML — 40.5%
- CSS — 3.5%
- JavaScript — 0.5%

## What has been done for Azure
- The API has been prepared to work in Azure App Service (files, environment configuration, support for environment variables/connection strings).
- The MVC frontend has been transferred to a Docker container (its own Dockerfile) to run independently of the API.
- Role separation has been created: the API manages access to the database, and the frontend makes HTTP requests to the API.
- Instructions for building, publishing images to the registry, and deploying to Azure have been added to the README.
- Recommendations for configuring environment variables, CORS, and secure storage of connection strings (Connection Strings / Managed Identity) have been provided.

Translated with DeepL.com (free version)

## Architecture
Client (browser) → Frontend (MVC in a separate container, Azure Container) → API (Azure App Service) → Database (Azure SQL / other selected service)

The frontend and API are deployed as independent units and communicate via HTTP(s). This allows them to be scaled separately and the frontend to be updated without deploying the API and vice versa.
