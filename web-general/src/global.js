const URLS = {
    PRODUCTION: {
        API: "https://api.prom.uploy.app",
        APP: "https://prom.uploy.app"
    },
    DEVELOPMENT: {
        API: "http://localhost:3333",
        APP: "http://localhost:3000"
    }
}

const CAMPAIGN_STATES = {
    ACTIVE: "Active",
    INACTIVE: "Inactive",
    SEALED: "Sealed",
    FINISHED: "Finished",
    FAILED: "Failed",
}

module.exports = {
    ROOT_URL_API: URLS.DEVELOPMENT.API,
    ROOT_URL_PORTAL: URLS.DEVELOPMENT.APP,
    CAMPAIGN_STATES: CAMPAIGN_STATES
}