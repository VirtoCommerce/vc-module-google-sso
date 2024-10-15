# Google Single Sign-On

## Overview

Google Single Sign-On (SSO) is a module that provides users with a simplified sign-in experience. It allows users to access multiple applications with a single set of credentials, eliminating the need to remember different usernames and passwords for each application.

Google SSO module integrates Virto Commerce with Google to provide secure authentication and authorization for cloud and on-premises applications. This helps to improve productivity, security, and user satisfaction by reducing the number of times users are prompted for their credentials.


## Features
* Google SSO for Virto Commerce Platform and Frontend.

## Screenshots
![image](https://github.com/user-attachments/assets/d4b7e292-7317-4bdd-98f8-7e9105ae3a9e)
![image](https://github.com/user-attachments/assets/9fb75bea-9161-4fb2-89e0-b1a6c92d36cd)
![image](https://github.com/user-attachments/assets/b6c31c1a-1f95-4de0-97e2-81a04908bde3)


## Setup

### Create Google OAuth 2.0 Client
To use Google APIs in an application with OAuth 2.0, you need authorization credentials that identify the app for Google's OAuth 2.0 server. Your applications will be able to use these credentials to access APIs that you have enabled for that project.

To create credentials for your project:

1. Go to [Google API & Services](https://console.cloud.google.com/apis).
1. Create a new project and open the dashboard.
1. In the OAuth consent screen of the dashboard:
    * Select User Type → External and click CREATE.
    * In the App Information dialogue, type the app name, user support email, and developer contact information.
    * Skip Scopes.
    * Skip Test users.
    * Review the OAuth consent screen and return to the app dashboard.
    * In the Credentials tab of the app dashboard, select CREATE CREDENTIALS > OAuth client ID.
1. Select Application type → Web application and choose a name.
1. In the Authorized redirect URIs section, select ADD URI to set the redirect URI (`https://{host}/signin-google`). Run the platform using the HTTPS scheme. Otherwise, the SSO won't work.

> Note: If your platform runs on a local machine, put https://localhost:10645/signin-google.

1. Click CREATE.
1. Save Client ID and Client Secret to use them in the module.

### Configure Google sign-in
Store Google Client ID, secret values and other sensitive settings in KeyVault Storage. In our example, we use the appsettings.json configuration file. Add the following section to the configuration:

```json
"GoogleSSO": {
    "Enabled": true,
    "ApplicationId": "<your Client ID>",
    "Secret": "<your Client Secret>"
}
```

### Enable Google for Virto Commerce Frontend
1. Go to Virto Commerce Platform, Stores, select a store 
1. Click Authentication widget and activate Google sign-in for the store.
1. Add store URL to the list of authorized redirect URIs (`https://{store-host}/signin-google`).

![image](https://github.com/user-attachments/assets/75c82454-0f43-4c2a-bada-8d20332fa9b9)

## References
* Home: https://virtocommerce.com
* Documentation: https://docs.virtocommerce.org
* Community: https://www.virtocommerce.org
* [Download Latest Release](https://github.com/VirtoCommerce/vc-module-google-sso/releases)

## License
Copyright (c) Virto Solutions LTD.  All rights reserved.

This software is licensed under the Virto Commerce Open Software License (the "License"); you
may not use this file except in compliance with the License. You may
obtain a copy of the License at http://virtocommerce.com/opensourcelicense.

Unless required by the applicable law or agreed to in written form, the software
distributed under the License is provided on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
implied.
