# Knative .Net Core Api Demo
Knative - .Net Core Api Demo App

It's mainly divided into 3 main steps
- Install necessary tools to prepare yourself for Knative style deployment on your local PC
- Create a Docker Image and Deploy it to your preferred cloud.  (I have used Azure)
- Create Knative cluster and deploy the image.  You can then perform necessary configuration changes to meet your needs.

--------


# Step 1 - Knative Readiness

## Install Kubectl
Download and Install Kubectl from https://kubernetes.io/docs/tasks/tools/install-kubectl-windows/

<kbd>![image](https://user-images.githubusercontent.com/30829678/187966291-e7a78efe-a9df-4fc5-a93a-39915a226b0c.png)</kbd>


## Install Knative client
Download and Install Knative client from https://github.com/knative/client/releases

<kbd>![image](https://user-images.githubusercontent.com/30829678/187966360-5873527f-7c8f-40b5-9a83-184101c28002.png)</kbd>


## Install Knative Quickstart Plugin
Download and Install Knative Quickstart Plugin from https://github.com/knative-sandbox/kn-plugin-quickstart/releases

<kbd>![image](https://user-images.githubusercontent.com/30829678/187966418-920f60e7-7e0f-4765-8e5b-1a296b9b5f9d.png)</kbd>


**Add the PATH Variable to have this access from anywhere**

Note:  I have put all the three executables in to docker executable folders to avoid adding PATH variables for all the three files.

<kbd>![image](https://user-images.githubusercontent.com/30829678/187967483-c6fc5b4c-7f57-41a1-aa3d-03ec0dab8a92.png)</kbd>

--------


# Step 2 - Create and Deploy Docker Image to your Preferred Cloud


## Create out of box .Net Core Api Project
- I will be using my existing .Net Core Api project to save some time.  You can refer to it here - https://github.com/vivekmvp/TodoApiWithVersioning
- I will be adding Docker Functionality on top of the existing functionality.

**For adding docker functionality**
Create a docker file by right clicking the project and add Docker Support (For Linux)

![image](https://user-images.githubusercontent.com/30829678/187998538-b3eabc32-a1e6-40e8-b68f-aa2f0e8a8398.png)


**Docker File content**

    #See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

    FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
    WORKDIR /app
    EXPOSE 80
    EXPOSE 443

    FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
    WORKDIR /src
    COPY ["TodoApiLatest/TodoApiLatest.csproj", "TodoApiLatest/"]
    RUN dotnet restore "TodoApiLatest/TodoApiLatest.csproj"
    COPY . .
    WORKDIR "/src/TodoApiLatest"
    RUN dotnet build "TodoApiLatest.csproj" -c Release -o /app/build

    FROM build AS publish
    RUN dotnet publish "TodoApiLatest.csproj" -c Release -o /app/publish /p:UseAppHost=false

    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "TodoApiLatest.dll"]



## Create Azure Container Restiry and deploy the image

- Create Azure Container Registry in Azure

<kbd>![image](https://user-images.githubusercontent.com/30829678/187999400-5f5b1728-425f-439c-9513-2a35eb57930f.png)</kbd>

<kbd>![image](https://user-images.githubusercontent.com/30829678/187999980-aae43ac1-46f3-46ed-8136-33ed4e2d1863.png)</kbd>

Deploying Local .Net Core Api to Azure Container Registry through Azure CLI Commands

- az login
- az acr build --registry <container_registry_name> --image <image_name> .

**Uploading Docker Image to Azure Registry**

<kbd>![image](https://user-images.githubusercontent.com/30829678/188002494-53a42c93-c487-4318-97b6-71d0013c2667.png)</kbd>


