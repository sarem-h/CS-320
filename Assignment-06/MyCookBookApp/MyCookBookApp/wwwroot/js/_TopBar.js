function toggleNavbar() {
    var navbar = document.getElementById('customNavbar');
    var overlay = document.getElementById('overlay');
    var menuContainer = document.getElementById('menuContainer');
    var topBarContainer = document.getElementById('topBarContainer');

    if (navbar && overlay && menuContainer && topBarContainer) {
        if (navbar.classList.contains('show')) {
            navbar.classList.remove('show');
            overlay.classList.remove('show');
            menuContainer.classList.remove('flex-column');
            menuContainer.classList.add('flex-row');
            topBarContainer.classList.remove('reduced-padding');
        } else {
            navbar.classList.add('show');
            overlay.classList.add('show');
            menuContainer.classList.remove('flex-row');
            menuContainer.classList.add('flex-column');
            topBarContainer.classList.add('reduced-padding');
        }
    }
}

// Close the custom collapse body when clicking outside of it
document.addEventListener('click', function(event) {
    var navbar = document.getElementById('customNavbar');
    var overlay = document.getElementById('overlay');
    var customCollapse = document.getElementById('customCollapse');
    var toggler = document.querySelector('.custom-toggler');
    var topBarContainer = document.getElementById('topBarContainer');
    if (navbar && overlay && customCollapse && toggler && topBarContainer) {
        if (!navbar.contains(event.target) && !toggler.contains(event.target)) {
            navbar.classList.remove('show');
            overlay.classList.remove('show');
            customCollapse.classList.remove('show');
            var menuContainer = document.getElementById('menuContainer');
            if (menuContainer) {
                menuContainer.classList.remove('flex-column');
                menuContainer.classList.add('flex-row');
                topBarContainer.classList.remove('reduced-padding');
            }
        }
    }
});

// Update the menu layout on window resize
window.addEventListener('resize', function() {
    var menuContainer = document.getElementById('menuContainer');
    var topBarContainer = document.getElementById('topBarContainer');
    if (menuContainer && topBarContainer) {
        if (window.innerWidth >= 992) {
            menuContainer.classList.remove('flex-column');
            menuContainer.classList.add('flex-row');
            topBarContainer.classList.remove('reduced-padding');
        } else {
            menuContainer.classList.remove('flex-row');
            menuContainer.classList.add('flex-column');
            topBarContainer.classList.add('reduced-padding');
        }
    }
});

// Initial layout setup
document.addEventListener('DOMContentLoaded', function() {
    var menuContainer = document.getElementById('menuContainer');
    var topBarContainer = document.getElementById('topBarContainer');
    if (menuContainer && topBarContainer) {
        if (window.innerWidth >= 992) {
            menuContainer.classList.add('flex-row');
            topBarContainer.classList.remove('reduced-padding');
        } else {
            menuContainer.classList.add('flex-column');
            topBarContainer.classList.add('reduced-padding');
        }
    }
});
