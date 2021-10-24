# aspnet-core-3-signup-verification-api

Welcome to my Real Estate Data Project. The intention of the project is to develop a public API to deliver KPI's and Raw Data about real estate offerings in certain areas. This project is an ASP.NET Core 3.1 API that is running severlessly on AWS Lambda, and uses a MS SQL Server instance hosted with AWS RDS. 

<h3>Public Endpoints</h3>
<h4>GET: https://6qhw0yv2fd.execute-api.us-east-1.amazonaws.com/realestate/rentals/ {string: Zip Code}</h4>

This endpoint returns a collection of properties available for rent by zip code. The endpoint returns standard information such as address, monthly rent, geo-location, property type and layout. In addition to that there are some calculated properties that are used for a more advanced analysis of the data such as absolute and normalized $/sqft that can be used to compare different floorplans/layouts.

<img
                src='https://github.com/tron5389/public-images/blob/master/RealEstateRentals_SwaggerDocumentation.PNG?raw=true'
                alt="person picture"
                />
                
<img
                src='https://github.com/tron5389/public-images/blob/master/RealEstateRentals_SwaggerExample.PNG?raw=true'
                alt="person picture"
                />

<h3>Architecture</h3>
As mentioned before all the resources for this project exist in AWS. In an AWS project all resources are configured inside your Virtual Private Cloud, which is like a virtual personal network. I created a VPC specifically for this project named 'my-vpc' as shown below. Inside the VPC there are 2 public and 2 private subnets that exist in two availability zones in the us-east-1 region. Inside the public subnet exists the MS SQL Server Express instance that stores all of the data collected for this API. Inside the private subnets exists the lambda application which is what is actually running the ASP.NET Core project. Theses function are invoked by making an http request to the API gateway which routs any type of request by proxy to the controllers running in the lambda function. A security group is configured around the entire VPC which allows inbound traffic to the public subnets so any developer that wants to join in the project can interact with the database.
<img
                src='https://github.com/tron5389/public-images/blob/master/API%20Diagram.png?raw=true'
                alt="person picture"
                />


                
This project utilizes some boiler plate code for ASP.NET Core Projects from a community developer. For more information and documentation on this great template please visit https://jasonwatmore.com/post/2020/07/06/aspnet-core-3-boilerplate-api-with-email-sign-up-verification-authentication-forgot-password
