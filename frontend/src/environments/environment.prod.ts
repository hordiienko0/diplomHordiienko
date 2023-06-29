const baseHost = "https://ctor-backend.azurewebsites.net"

export const environment = {
  production: true,
  apiHost: baseHost.replace("https://", ""),
  apiHostWithHttp: baseHost,
  apiBaseUrl: `${baseHost}/api`,
  filesBaseUrl: `${baseHost}/files`
};


