here we are implementing JWT token for our application in .net 8 or previous versions

So firstly lets talk what is JWT Token, Basically it is token used to authenticate your API's. it contains header, payload , signature.

The header typically consists of two parts:
1- The type of token, which is JWT.
2- The signing algorithm being used, such as HMAC SHA256 or RSA.  

Payload consists of the session data called as claims. Below are some of the standard claims that we can use, 
1. Issuer(iss)
2. Subject (sub)
3. Audience (aud)
4. Expiration time (exp)
5. Issued at (iat)

Signature is calculated by encoding the header and payload using Base64url Encoding and concatenating them with a period separator.

So lets carried out the steps you will implement in your project for JWT Token.

Step 1- You need to add nuget package in your project "System.IdentityModel.Tokens.Jwt".
Step 2-Add code written in Shared/TokenHelper class in your project. here you will find functions for creating and validating your jwt token.
Simply call the functions and check how the are working.


Next add custom middleware to Authorize your requests, You will find configurations in program.cs
