// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
//
// Lightweight UI enhancements for the dashboard shell.

(function () {
    function initSidebarToggle() {
        var sidebar = document.getElementById("appSidebar");
        var toggle = document.getElementById("sidebarCollapseToggle");
        var COLLAPSED_CLASS = "is-collapsed";
        // Bump the key version so old saved state (from previous UI) is ignored
        var STORAGE_KEY = "bms.sidebar.v2.collapsed";

        if (!sidebar || !toggle) {
            return;
        }

        // Always start expanded on desktop for this version
        if (window.innerWidth >= 992) {
            sidebar.classList.remove(COLLAPSED_CLASS);
        }

        // Restore persisted state (for v2 and onward)
        try {
            var saved = window.localStorage.getItem(STORAGE_KEY);
            if (saved === "1") {
                sidebar.classList.add(COLLAPSED_CLASS);
            }
        } catch (e) {
            // Storage not essential; fail silently.
        }

        toggle.addEventListener("click", function () {
            var isCollapsed = sidebar.classList.toggle(COLLAPSED_CLASS);
            try {
                window.localStorage.setItem(STORAGE_KEY, isCollapsed ? "1" : "0");
            } catch (e) {
                // Ignore quota / privacy errors.
            }
        });
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", initSidebarToggle);
    } else {
        initSidebarToggle();
    }
})();


