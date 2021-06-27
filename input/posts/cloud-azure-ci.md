Title: Azure Cloud CLI Windows Container Instance
Lead: Hands-on features of Azure CLI, account, group, container etc
Published: 08/15/2018
Tags:
  - Azure Cloud
  - System Adiministration
---

This article demos various features of Azure CLI.

First, we set the path,

    Microsoft Windows [Version 10.0.17134.165]
    (c) 2018 Microsoft Corporation. All rights reserved.
    > set PATH=%PATH%;C:\Program Files (x86)\Microsoft SDKs\Azure\CLI2\wbin

Then we check current tools versions,

    > az --version
    azure-cli (2.0.43)

    acr (2.1.2)
    acs (2.2.2)
    advisor (0.6.0)
    ams (0.2.1)
    appservice (0.2.1)
    backup (1.2.0)
    batch (3.3.1)
    batchai (0.4.0)
    billing (0.2.0)
    cdn (0.1.0)
    cloud (2.1.0)
    cognitiveservices (0.2.0)
    command-modules-nspkg (2.0.2)
    configure (2.0.18)
    consumption (0.4.0)
    container (0.3.2)
    core (2.0.43)
    cosmosdb (0.2.0)
    dla (0.2.0)
    dls (0.1.0)
    dms (0.1.0)
    eventgrid (0.2.0)
    eventhubs (0.2.1)
    extension (0.2.1)
    feedback (2.1.4)
    find (0.2.12)
    interactive (0.3.27)
    iot (0.2.0)
    keyvault (2.2.1)
    lab (0.1.0)
    maps (0.3.1)
    monitor (0.2.1)
    network (2.2.2)
    nspkg (3.0.3)
    policyinsights (0.1.0)
    profile (2.1.1)
    rdbms (0.3.0)
    redis (0.3.0)
    reservations (0.3.1)
    resource (2.1.1)
    role (2.1.2)
    search (0.1.0)
    servicebus (0.2.1)
    servicefabric (0.1.0)
    sql (2.1.1)
    storage (2.1.1)
    vm (2.2.0)

Python location `C:\Program Files (x86)\Microsoft SDKs\Azure\CLI2\python.exe`
Extensions directory `C:\Users\Atiq\.azure\cliextensions`

Python version,

    Python (Windows) 3.6.5 (v3.6.5:f59c0932b4, Mar 28 2018, 16:07:46) [MSC v.1900 32 bit (Intel)]
    Legal docs and information: aka.ms/AzureCliLegal


Let's login,

    > az login
    Note, we have launched a browser for you to login. For old experience with device code, use "az login --use-device-code"
    You have logged in. Now let us find all the subscriptions to which you have access...
    [
    {
        "cloudName": "AzureCloud",
        "id": "b2faf878-6232-4834-8e65-5e2baeab5939",
        "isDefault": true,
        "name": "Free Trial",
        "state": "Enabled",
        "tenantId": "cbd5d855-bf04-4fa4-8da1-a3282cdb742b",
        "user": {
        "name": "sodadrinkerthree@hotmail.com",
        "type": "user"
        }
    }
    ]

List accounts and VM,

    > az account list
    [
    {
        "cloudName": "AzureCloud",
        "id": "b2faf878-6232-4834-8e65-5e2baeab5939",
        "isDefault": true,
        "name": "Free Trial",
        "state": "Enabled",
        "tenantId": "cbd5d855-bf04-4fa4-8da1-a3282cdb742b",
        "user": {
        "name": "sodadrinkerthree@hotmail.com",
        "type": "user"
        }
    }
    ]

Create a container,

    > az group create --name docker --location westus
    {
    "id": "/subscriptions/b2faf878-6232-4834-8e65-5e2baeab5939/resourceGroups/docker",
    "location": "westus",
    "managedBy": null,
    "name": "docker",
    "properties": {
        "provisioningState": "Succeeded"
    },
    "tags": null
    }

create regular nanoserver instance,

    > az container create --name winci --image microsoft/nanoserver --resource-group docker --ip-address public --os-type Windows --location westus

List resource groups,

    > az container list --resource-group docker -o table
    Name    ResourceGroup    ProvisioningState    Image                 IP:ports           CPU/Memory       OsType    Location
    ------  ---------------  -------------------  --------------------  -----------------  ---------------  --------  ----------
    winci   docker           Succeeded            microsoft/nanoserver  40.118.229.222:80  1.0 core/1.5 gb  Windows   westus

delete its resource group,

    > az container delete --resource-group docker --name winci

Create a CI from nanoserver IIS image, this one does recognize executing commands,

    > az container create --name winci --image microsoft/iis:nanoserver --resource-group docker --ip-address public --os-type Windows --location westus --ports 80
    {
    "containers": [
        {
        "command": null,
        "environmentVariables": [],
        "image": "microsoft/iis:nanoserver",
        "instanceView": {
            "currentState": {
            "detailStatus": "",
            "exitCode": null,
            "finishTime": null,
            "startTime": "2018-08-09T05:17:14+00:00",
            "state": "Running"
            },
            "events": [
            {
                "count": 2,
                "firstTimestamp": "2018-08-09T05:15:33+00:00",
                "lastTimestamp": "2018-08-09T05:16:02+00:00",
                "message": "pulling image \"microsoft/iis:nanoserver\"",
                "name": "Pulling",
                "type": "Normal"
            },
            {
                "count": 1,
                "firstTimestamp": "2018-08-09T05:17:05+00:00",
                "lastTimestamp": "2018-08-09T05:17:05+00:00",
                "message": "Successfully pulled image \"microsoft/iis:nanoserver\"",
                "name": "Pulled",
                "type": "Normal"
            },
            {
                "count": 1,
                "firstTimestamp": "2018-08-09T05:17:05+00:00",
                "lastTimestamp": "2018-08-09T05:17:05+00:00",
                "message": "Created container with docker id 7f89eb798bff",
                "name": "Created",
                "type": "Normal"
            },
            {
                "count": 1,
                "firstTimestamp": "2018-08-09T05:17:14+00:00",
                "lastTimestamp": "2018-08-09T05:17:14+00:00",
                "message": "Started container with docker id 7f89eb798bff",
                "name": "Started",
                "type": "Normal"
            }
            ],
            "previousState": null,
            "restartCount": 0
        },
        "livenessProbe": null,
        "name": "winci",
        "ports": [
            {
            "port": 80,
            "protocol": "TCP"
            }
        ],
        "readinessProbe": null,
        "resources": {
            "limits": null,
            "requests": {
            "cpu": 1.0,
            "memoryInGb": 1.5
            }
        },
        "volumeMounts": null
        }
    ],
    "diagnostics": null,
    "id": "/subscriptions/b2faf878-6232-4834-8e65-5e2baeab5939/resourceGroups/docker/providers/Microsoft.ContainerInstance/containerGroups/winci",
    "imageRegistryCredentials": null,
    "instanceView": {
        "events": [],
        "state": "Running"
    },
    "ipAddress": {
        "dnsNameLabel": null,
        "fqdn": null,
        "ip": "40.78.17.202",
        "ports": [
        {
            "port": 80,
            "protocol": "TCP"
        }
        ]
    },
    "location": "westus",
    "name": "winci",
    "osType": "Windows",
    "provisioningState": "Succeeded",
    "resourceGroup": "docker",
    "restartPolicy": "Always",
    "tags": {},
    "type": "Microsoft.ContainerInstance/containerGroups",
    "volumes": null
    }

Now, lets execute,

    > az container exec --resource-group docker --name winci --exec-command "cmd.exe"
    ... messed up output contains ASCII 0x1B ....
    .. Microsoft Windows [Version 10.0.14393] ....(c) 2016 Micro...

Environment,

    C:\>set
    ALLUSERSPROFILE=C:\ProgramData
    APPDATA=C:\Users\ContainerAdministrator\AppData\Roaming
    CAAS_3F2A3A4754DE4E56877D945C622B7D50_PORT=tcp://10.0.225.18:80
    CAAS_3F2A3A4754DE4E56877D945C622B7D50_PORT_80_TCP=tcp://10.0.225.18:80
    CAAS_3F2A3A4754DE4E56877D945C622B7D50_PORT_80_TCP_ADDR=10.0.225.18
    CAAS_3F2A3A4754DE4E56877D945C622B7D50_PORT_80_TCP_PORT=80
    CAAS_3F2A3A4754DE4E56877D945C622B7D50_PORT_80_TCP_PROTO=tcp
    CAAS_3F2A3A4754DE4E56877D945C622B7D50_SERVICE_HOST=10.0.225.18
    CAAS_3F2A3A4754DE4E56877D945C622B7D50_SERVICE_PORT=80
    CAAS_3F2A3A4754DE4E56877D945C622B7D50_SERVICE_PORT_80=80
    CommonProgramFiles=C:\Program Files\Common Files
    CommonProgramFiles(x86)=C:\Program Files (x86)\Common Files
    CommonProgramW6432=C:\Program Files\Common Files
    COMPUTERNAME=7F89EB798BFF
    ComSpec=C:\Windows\system32\cmd.exe
    KUBERNETES_PORT=tcp://10.0.0.1:443
    KUBERNETES_PORT_443_TCP=tcp://10.0.0.1:443
    KUBERNETES_PORT_443_TCP_ADDR=10.0.0.1
    KUBERNETES_PORT_443_TCP_PORT=443
    KUBERNETES_PORT_443_TCP_PROTO=tcp
    KUBERNETES_SERVICE_HOST=10.0.0.1
    KUBERNETES_SERVICE_PORT=443
    KUBERNETES_SERVICE_PORT_HTTPS=443
    LOCALAPPDATA=C:\Users\ContainerAdministrator\AppData\Local
    NUMBER_OF_PROCESSORS=2
    OS=Windows_NT
    Path=C:\Windows\system32;C:\Windows;C:\Windows\System32\Wbem;C:\Windows\System32\WindowsPowerShell\v1.0\;C:\Users\ContainerAdministrator\AppData\Local\Microsoft\WindowsApps
    PATHEXT=.COM;.EXE;.BAT;.CMD
    PROCESSOR_ARCHITECTURE=AMD64
    PROCESSOR_IDENTIFIER=Intel64 Family 6 Model 63 Stepping 2, GenuineIntel
    PROCESSOR_LEVEL=6
    PROCESSOR_REVISION=3f02
    ProgramData=C:\ProgramData
    ProgramFiles=C:\Program Files
    ProgramFiles(x86)=C:\Program Files (x86)
    ProgramW6432=C:\Program Files
    PROMPT=$P$G
    PUBLIC=C:\Users\Public
    SystemDrive=C:
    SystemRoot=C:\Windows
    TEMP=C:\Users\ContainerAdministrator\AppData\Local\Temp
    TMP=C:\Users\ContainerAdministrator\AppData\Local\Temp
    USERDOMAIN=User Manager
    USERNAME=ContainerAdministrator
    USERPROFILE=C:\Users\ContainerAdministrator
    windir=C:\Windows

list acounts,

    > az account show
    {
    "environmentName": "AzureCloud",
    "id": "b2faf878-6232-4834-8e65-5e2baeab5939",
    "isDefault": true,
    "name": "Free Trial",
    "state": "Enabled",
    "tenantId": "cbd5d855-bf04-4fa4-8da1-a3282cdb742b",
    "user": {
        "name": "sodadrinkerthree@hotmail.com",
        "type": "user"
    }
    }

List containers in 2 ways,

    > az container list --out table
    Name    ResourceGroup    ProvisioningState    Image                     IP:ports         CPU/Memory       OsType    Location
    ------  ---------------  -------------------  ------------------------  ---------------  ---------------  --------  ----------
    winci   docker           Succeeded            microsoft/iis:nanoserver  40.78.17.202:80  1.0 core/1.5 gb  Windows   westus

    az> container list
    [
    {
        "containers": [
        {
            "command": null,
            "environmentVariables": [],
            "image": "microsoft/iis:nanoserver",
            "instanceView": null,
            "livenessProbe": null,
            "name": "winci",
            "ports": [
            {
                "port": 80,
                "protocol": "TCP"
            }
            ],
            "readinessProbe": null,
            "resources": {
            "limits": null,
            "requests": {
                "cpu": 1.0,
                "memoryInGb": 1.5
            }
            },
            "volumeMounts": null
        }
        ],
        "diagnostics": null,
        "id": "/subscriptions/b2faf878-6232-4834-8e65-5e2baeab5939/resourceGroups/docker/providers/Microsoft.ContainerInstance/containerGroups/winci",
        "imageRegistryCredentials": null,
        "instanceView": null,
        "ipAddress": {
        "dnsNameLabel": null,
        "fqdn": null,
        "ip": "40.78.17.202",
        "ports": [
            {
            "port": 80,
            "protocol": "TCP"
            }
        ]
        },
        "location": "westus",
        "name": "winci",
        "osType": "Windows",
        "provisioningState": "Succeeded",
        "resourceGroup": "docker",
        "restartPolicy": "Always",
        "tags": {},
        "type": "Microsoft.ContainerInstance/containerGroups",
        "volumes": null
    }
    ]

List web apps,

    az>> webapp list
    [
    {
        "appServicePlanId": "/subscriptions/b2faf878-6232-4834-8e65-5e2baeab5939/resourceGroups/blog/providers/Microsoft.Web/serverfarms/WestUS-D1-Shared",
        "availabilityState": "Normal",
        "clientAffinityEnabled": true,
        "clientCertEnabled": false,
        "cloningInfo": null,
        "containerSize": 0,
        "dailyMemoryTimeQuota": 0,
        "defaultHostName": "orchardcore.azurewebsites.net",
        "enabled": true,
        "enabledHostNames": [
        "orchardcore.azurewebsites.net",
        "orchardcore.scm.azurewebsites.net"
        ],
        "hostNameSslStates": [
        {
            "hostType": "Standard",
            "ipBasedSslResult": null,
            "ipBasedSslState": "NotConfigured",
            "name": "orchardcore.azurewebsites.net",
            "sslState": "Disabled",
            "thumbprint": null,
            "toUpdate": null,
            "toUpdateIpBasedSsl": null,
            "virtualIp": null
        },
        {
            "hostType": "Repository",
            "ipBasedSslResult": null,
            "ipBasedSslState": "NotConfigured",
            "name": "orchardcore.scm.azurewebsites.net",
            "sslState": "Disabled",
            "thumbprint": null,
            "toUpdate": null,
            "toUpdateIpBasedSsl": null,
            "virtualIp": null
        }
        ],
        "hostNames": [
        "orchardcore.azurewebsites.net"
        ],
        "hostNamesDisabled": false,
        "hostingEnvironmentProfile": null,
        "httpsOnly": false,
        "id": "/subscriptions/b2faf878-6232-4834-8e65-5e2baeab5939/resourceGroups/blog/providers/Microsoft.Web/sites/orchardcore",
        "identity": null,
        "isDefaultContainer": null,
        "kind": "app",
        "lastModifiedTimeUtc": "2018-07-27T20:07:09.590000",
        "location": "West US",
        "maxNumberOfWorkers": null,
        "name": "orchardcore",
        "outboundIpAddresses": "104.42.152.64,40.118.228.30,40.118.235.170,40.118.239.187,40.118.232.194",
        "possibleOutboundIpAddresses": "104.42.152.64,40.118.228.30,40.118.235.170,40.118.239.187,40.118.232.194,40.118.232.202,40.118.239.1,40.112.131.175",
        "repositorySiteName": "orchardcore",
        "reserved": false,
        "resourceGroup": "blog",
        "scmSiteAlsoStopped": false,
        "siteConfig": null,
        "slotSwapStatus": null,
        "snapshotInfo": null,
        "state": "Running",
        "suspendedTill": null,
        "tags": {
        "hidden-related:/subscriptions/b2faf878-6232-4834-8e65-5e2baeab5939/resourcegroups/blog/providers/Microsoft.Web/serverfarms/WestUS-D1-Shared": "empty"
        },
        "targetSwapSlot": null,
        "trafficManagerHostNames": null,
        "type": "Microsoft.Web/sites",
        "usageState": "Normal"
    }
    ]

list accounts,

    az>> az account list
    [
    {
        "cloudName": "AzureCloud",
        "id": "b2faf878-6232-4834-8e65-5e2baeab5939",
        "isDefault": true,
        "name": "Free Trial",
        "state": "Enabled",
        "tenantId": "cbd5d855-bf04-4fa4-8da1-a3282cdb742b",
        "user": {
        "name": "sodadrinkerthree@hotmail.com",
        "type": "user"
        }
    }
    ]

List locations,

  az>> az appservice list-locations --sku F1
  [
    {
      "name": "Central US"
    },
    {
      "name": "North Europe"
    },
    {
      "name": "West Europe"
    },
    {
      "name": "Southeast Asia"
    },
    {
      "name": "East Asia"
    },
    {
      "name": "West US"
    },
    {
      "name": "East US"
    },
    {
      "name": "Japan West"
    },
    {
      "name": "Japan East"
    },
    {
      "name": "East US 2"
    },
    {
      "name": "North Central US"
    },
    {
      "name": "South Central US"
    },
    {
      "name": "Brazil South"
    },
    {
      "name": "Australia East"
    },
    {
      "name": "Australia Southeast"
    },
    {
      "name": "Central India"
    },
    {
      "name": "West India"
    },
    {
      "name": "South India"
    },
    {
      "name": "Canada Central"
    },
    {
      "name": "Canada East"
    },
    {
      "name": "West Central US"
    },
    {
      "name": "West US 2"
    },
    {
      "name": "UK West"
    },
    {
      "name": "UK South"
    },
    {
      "name": "Korea South"
    },
    {
      "name": "Korea Central"
    },
    {
      "name": "France South"
    },
    {
      "name": "France Central"
    }
  ]

List available groups,

    az>> az group list
    [
    {
        "id": "/subscriptions/b2faf878-6232-4834-8e65-5e2baeab5939/resourceGroups/asynctest",
        "location": "westus",
        "managedBy": null,
        "name": "asynctest",
        "properties": {
        "provisioningState": "Succeeded"
        },
        "tags": null
    },
    {
        "id": "/subscriptions/b2faf878-6232-4834-8e65-5e2baeab5939/resourceGroups/blog",
        "location": "centralus",
        "managedBy": null,
        "name": "blog",
        "properties": {
        "provisioningState": "Succeeded"
        },
        "tags": null
    },
    {
        "id": "/subscriptions/b2faf878-6232-4834-8e65-5e2baeab5939/resourceGroups/docker",
        "location": "westus",
        "managedBy": null,
        "name": "docker",
        "properties": {
        "provisioningState": "Succeeded"
        },
        "tags": null
    },
    {
        "id": "/subscriptions/b2faf878-6232-4834-8e65-5e2baeab5939/resourceGroups/ML",
        "location": "westus2",
        "managedBy": null,
        "name": "ML",
        "properties": {
        "provisioningState": "Succeeded"
        },
        "tags": null
    },
    {
        "id": "/subscriptions/b2faf878-6232-4834-8e65-5e2baeab5939/resourceGroups/Speech",
        "location": "westus",
        "managedBy": null,
        "name": "Speech",
        "properties": {
        "provisioningState": "Succeeded"
        },
        "tags": null
    }
    ]

list end points,

    az>> az cloud list
    [
    {
        "endpoints": {
        "activeDirectory": "https://login.microsoftonline.com",
        "activeDirectoryDataLakeResourceId": "https://datalake.azure.net/",
        "activeDirectoryGraphResourceId": "https://graph.windows.net/",
        "activeDirectoryResourceId": "https://management.core.windows.net/",
        "batchResourceId": "https://batch.core.windows.net/",
        "gallery": "https://gallery.azure.com/",
        "management": "https://management.core.windows.net/",
        "resourceManager": "https://management.azure.com/",
        "sqlManagement": "https://management.core.windows.net:8443/",
        "vmImageAliasDoc": "https://raw.githubusercontent.com/Azure/azure-rest-api-specs/master/arm-compute/quickstart-templates/aliases.json"
        },
        "isActive": true,
        "name": "AzureCloud",
        "profile": "latest",
        "suffixes": {
        "acrLoginServerEndpoint": ".azurecr.io",
        "azureDatalakeAnalyticsCatalogAndJobEndpoint": "azuredatalakeanalytics.net",
        "azureDatalakeStoreFileSystemEndpoint": "azuredatalakestore.net",
        "keyvaultDns": ".vault.azure.net",
        "sqlServerHostname": ".database.windows.net",
        "storageEndpoint": "core.windows.net"
        }
    },
    {
        "endpoints": {
        "activeDirectory": "https://login.chinacloudapi.cn",
        "activeDirectoryDataLakeResourceId": null,
        "activeDirectoryGraphResourceId": "https://graph.chinacloudapi.cn/",
        "activeDirectoryResourceId": "https://management.core.chinacloudapi.cn/",
        "batchResourceId": "https://batch.chinacloudapi.cn/",
        "gallery": "https://gallery.chinacloudapi.cn/",
        "management": "https://management.core.chinacloudapi.cn/",
        "resourceManager": "https://management.chinacloudapi.cn",
        "sqlManagement": "https://management.core.chinacloudapi.cn:8443/",
        "vmImageAliasDoc": "https://raw.githubusercontent.com/Azure/azure-rest-api-specs/master/arm-compute/quickstart-templates/aliases.json"
        },
        "isActive": false,
        "name": "AzureChinaCloud",
        "profile": "latest",
        "suffixes": {
        "acrLoginServerEndpoint": ".azurecr.cn",
        "azureDatalakeAnalyticsCatalogAndJobEndpoint": null,
        "azureDatalakeStoreFileSystemEndpoint": null,
        "keyvaultDns": ".vault.azure.cn",
        "sqlServerHostname": ".database.chinacloudapi.cn",
        "storageEndpoint": "core.chinacloudapi.cn"
        }
    },
    {
        "endpoints": {
        "activeDirectory": "https://login.microsoftonline.us",
        "activeDirectoryDataLakeResourceId": null,
        "activeDirectoryGraphResourceId": "https://graph.windows.net/",
        "activeDirectoryResourceId": "https://management.core.usgovcloudapi.net/",
        "batchResourceId": "https://batch.core.usgovcloudapi.net/",
        "gallery": "https://gallery.usgovcloudapi.net/",
        "management": "https://management.core.usgovcloudapi.net/",
        "resourceManager": "https://management.usgovcloudapi.net/",
        "sqlManagement": "https://management.core.usgovcloudapi.net:8443/",
        "vmImageAliasDoc": "https://raw.githubusercontent.com/Azure/azure-rest-api-specs/master/arm-compute/quickstart-templates/aliases.json"
        },
        "isActive": false,
        "name": "AzureUSGovernment",
        "profile": "latest",
        "suffixes": {
        "acrLoginServerEndpoint": ".azurecr.us",
        "azureDatalakeAnalyticsCatalogAndJobEndpoint": null,
        "azureDatalakeStoreFileSystemEndpoint": null,
        "keyvaultDns": ".vault.usgovcloudapi.net",
        "sqlServerHostname": ".database.usgovcloudapi.net",
        "storageEndpoint": "core.usgovcloudapi.net"
        }
    },
    {
        "endpoints": {
        "activeDirectory": "https://login.microsoftonline.de",
        "activeDirectoryDataLakeResourceId": null,
        "activeDirectoryGraphResourceId": "https://graph.cloudapi.de/",
        "activeDirectoryResourceId": "https://management.core.cloudapi.de/",
        "batchResourceId": "https://batch.cloudapi.de/",
        "gallery": "https://gallery.cloudapi.de/",
        "management": "https://management.core.cloudapi.de/",
        "resourceManager": "https://management.microsoftazure.de",
        "sqlManagement": "https://management.core.cloudapi.de:8443/",
        "vmImageAliasDoc": "https://raw.githubusercontent.com/Azure/azure-rest-api-specs/master/arm-compute/quickstart-templates/aliases.json"
        },
        "isActive": false,
        "name": "AzureGermanCloud",
        "profile": "latest",
        "suffixes": {
        "acrLoginServerEndpoint": null,
        "azureDatalakeAnalyticsCatalogAndJobEndpoint": null,
        "azureDatalakeStoreFileSystemEndpoint": null,
        "keyvaultDns": ".vault.microsoftazure.de",
        "sqlServerHostname": ".database.cloudapi.de",
        "storageEndpoint": "core.cloudapi.de"
        }
    }
    ]
