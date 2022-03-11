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
    ACTIVE: "ACTIVE",
    INACTIVE: "INACTIVE",
    SEALED: "SEALED",
    FINISHED: "FINISHED",
    FAILED: "FAILED",
}

module.exports = {
    ROOT_URL_API: URLS.DEVELOPMENT.API,
    ROOT_URL_PORTAL: URLS.DEVELOPMENT.APP,
    CAMPAIGN_STATES: CAMPAIGN_STATES
}