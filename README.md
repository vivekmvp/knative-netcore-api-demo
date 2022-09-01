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

![image](https://user-images.githubusercontent.com/30829678/187966291-e7a78efe-a9df-4fc5-a93a-39915a226b0c.png)


## Install Knative client
Download and Install Knative client from https://github.com/knative/client/releases

![image](https://user-images.githubusercontent.com/30829678/187966360-5873527f-7c8f-40b5-9a83-184101c28002.png)


## Install Knative Quickstart Plugin
Download and Install Knative Quickstart Plugin from https://github.com/knative-sandbox/kn-plugin-quickstart/releases

![image](https://user-images.githubusercontent.com/30829678/187966418-920f60e7-7e0f-4765-8e5b-1a296b9b5f9d.png)


**Add the PATH Variable to have this access from anywhere**

Note:  I have put all the three executables in to docker executable folders to avoid adding PATH variables for all the three files.

![image](https://user-images.githubusercontent.com/30829678/187967483-c6fc5b4c-7f57-41a1-aa3d-03ec0dab8a92.png)

--------


# Step 2 - Create and Deploy Docker Image to your Preferred Cloud


