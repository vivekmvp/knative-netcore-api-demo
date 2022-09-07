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

## Install Kind
Note:  [kind](https://kind.sigs.k8s.io/) is a tool for running local Kubernetes clusters using Docker container “nodes”.  kind was primarily designed for testing Kubernetes itself, but may be used for local development or CI.

*Run following command to download the kind.exe*

`curl.exe -Lo kind-windows-amd64.exe https://kind.sigs.k8s.io/dl/v0.14.0/kind-windows-amd64`

![image](https://user-images.githubusercontent.com/30829678/188011995-2a248ce7-8afb-4d49-b955-cc85e6fb20f4.png)

And then place it to your preferred location.

**Add the PATH Variable to have this access from anywhere**

Note:  I have put all the three executables in to **C:\knative_readiness** folders and then add the folder to PATH variables to be able access from anywhere.

<kbd>![image](https://user-images.githubusercontent.com/30829678/188012915-b955bef4-d4a8-4c96-8dc6-c5c1ab5bdc37.png)</kbd>

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

<kbd>![image](https://user-images.githubusercontent.com/30829678/188003030-4aa6ad4a-ebbd-439d-a405-e2caed2b6f03.png)</kbd>

<kbd>![image](https://user-images.githubusercontent.com/30829678/188005195-807db04c-aed2-4583-a2ed-3362a09b68df.png)</kbd>



--------


# Step 3 - Create Knative Cluster and Deploy Knative service and use other features (Autoscale, etc)


## Create Knative cluster

- Before running below command, please make sure Docker Desktop is running otherwise you will get error.

`kn quickstart kind`

![image](https://user-images.githubusercontent.com/30829678/188966053-e58ccb99-306d-4154-b5f8-add1b31c18d5.png)


For some reason, I wasn't able to successfully run all steps..  It stops everytime at below step:

![image](https://user-images.githubusercontent.com/30829678/188971568-45fca0fd-5e6b-495a-8961-010f01da3a43.png)

but when I run the below command, I have noticed that knative cluster was created successfully.

`kubectl cluster-info --context kind-knative`

`kind get clusters`

![image](https://user-images.githubusercontent.com/30829678/188974358-45a4fee1-e873-495e-9c76-f096be654741.png)



## Deploying a Knative Service

- We will be deploying Azure container we created on knative
- Step 1 - We will create todoapi.yaml file with following content
- Step 2 - We will run the command to create knative service `kubectl apply -f todoapi.yaml`

        apiVersion: serving.knative.dev/v1
        kind: Service
        metadata:
          name: todoapi
        spec:
          template:
            spec:
              containers:
                - image: knativedemoapp.azurecr.io/todoapilatest
                  ports:
                    - containerPort: 443

![image](https://user-images.githubusercontent.com/30829678/188982853-ca76e206-b27c-4df7-ba3e-2d46c831c7de.png)

![image](https://user-images.githubusercontent.com/30829678/188983449-1e8d3ddc-99eb-4ad1-87cf-fb7ee52f0262.png)


**List all Knative Service**

Looks like Knative services are not working for me as expected.  I will need to research this further.

![image](https://user-images.githubusercontent.com/30829678/188987962-484e29f8-6b1b-430b-9b95-72b25d949b9b.png)

![image](https://user-images.githubusercontent.com/30829678/188985915-cf48cc90-c66e-4781-8638-b22ba30de89e.png)
