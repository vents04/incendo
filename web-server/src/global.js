const URLS = {
    PRODUCTION: {
        API: "https://api.prom.uploy.app",
        APP: "https://prom.uploy.app"
    },
    DEVELOPMENT: {
        API: "http://localhost:13353/api",
        APP: "http://localhost:3000"
    }
}

module.exports = {
    ROOT_URL_API: URLS.DEVELOPMENT.API,
    ROOT_URL_PORTAL: URLS.DEVELOPMENT.APP,
    AUTHENTICATION_TOKEN_KEY: "Authorization",
    MILLISECONDS_IN_A_MINUTE: 60000,
    CAMPAIGN_TYPES: {
        SELECT: "Select",
        SHUFFLE: "Shuffle",
        ASSIGN: "Assign"
    },
    CAMPAIGN_STATES: {
        INACTIVE: "Inactive",
        ACTIVE: "Active",
        SEALED: "Sealed",
        FINISHED: "Finished",
        FAILED: "Failed"
    }
}