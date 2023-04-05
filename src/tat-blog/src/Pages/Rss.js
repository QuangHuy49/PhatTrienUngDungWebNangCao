import React, { useEffect } from "react";

const Rss = () => {
    useEffect(() => {
        document.title = "Rss"
    }, []);

    return (
        <h1>
            Rss Feed
        </h1>
    )
}

export default Rss;