const PROXY_CONFIG = [
  {
    context: [
      "/score" // Adjust this to match your API endpoint
    ],
    target: "http://localhost:5014", // URL of your .NET Core backend
    secure: false
  }
];

console.log("Proxy config: ", PROXY_CONFIG)
module.exports = PROXY_CONFIG;
