# Library Management API

This project implements a RESTful API for managing a library of books using .NET, Entity Framework Core, and MSSQL Server. 
The infrastructure is automated with Terraform, and a CI/CD pipeline is defined using GitHub Actions.
The API is designed for deployment to AWS Fargate but can run locally for this assignment.

## Table of Contents

1. [Overview](#overview)
2. [Prerequisites](#prerequisites)
3. [Setup and Run Locally](#setup-and-run-locally)
4. [Endpoints](#endpoints)
5. [Authentication](#authentication)
6. [Infrastructure Automation](#infrastructure-automation)
7. [CI/CD Pipeline](#ci-cd-pipeline)

---

## Overview

Developed and deployed a RESTful API for managing a library of books using AWS Fargate.
Automated infrastructure provisioning with Terraform and implemented a CI/CD pipeline utilizing GitHub Actions.
The application followed a clean architecture structure, comprising API, Domain, Infrastructure, and Application layers.
Implemented a Generic Repository Pattern to reduce code redundancy and improve maintainability, alongside a Unit of Work to handle transaction management effectively.
Additionally, I created a generic middleware to handle exception management and integrated Serilog for robust logging, including support for correlation IDs.
Optimized performance by integrating an object mapper and enhanced configuration management through custom extension methods.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [MSSQL Server]()
- [Terraform](https://www.terraform.io/downloads)

---

## Setup and Run Locally
- Create Database
- Create The Tables (Manual or by Migartion)
- Open Visual Studio
- Select Docker Profile and Run
- TerraForm is not currently available in my region to test.

## Infrastructure Automation
- Initialize Terraform
Run the following command to initialize Terraform:
```bash
	terraform init
```

- plan and apply
```bash
	terraform plan -out=plan.out
	terraform apply plan.out
```

## CI/CD Pipeline
Just push the code to 'master' branch
ci-cd.yml file created with all the jobs
secrets are stored in githu secrets