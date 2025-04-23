document.addEventListener('DOMContentLoaded', function() {
    function debounce(func, wait) {
        let timeout;
        return function(...args) {
            const context = this;
            clearTimeout(timeout);
            timeout = setTimeout(() => func.apply(context, args), wait);
        };
    }

    const searchBarForm = document.getElementById('search-bar');
    const inputId = searchBarForm.getAttribute('data-input-id');
    const dropdownId = searchBarForm.getAttribute('data-dropdown-id');

    const searchInput = document.getElementById(inputId);
    const dropdown = document.getElementById(dropdownId);

    // Check if the event listener has already been added
    if (!searchInput.dataset.listenerAdded) {
        searchInput.addEventListener('input', debounce(function() {
            const query = searchInput.value;
            if (query.length > 0) {
                dropdown.innerHTML = ''; // Clear dropdown before fetching new results
                let foundRecipes = false;

                // Fetch from RecipeController
                fetch(`http://localhost:5085/api/recipe?search=${query}`)
                .then(response => response.json())
                .then(data => {
                    if (data.length > 0) {
                        foundRecipes = true;
                        const alexRecipesHeader = document.createElement('li');
                        alexRecipesHeader.className = 'dropdown-header';
                        alexRecipesHeader.textContent = "Alex's Recipes";
                        dropdown.appendChild(alexRecipesHeader);

                        data.forEach(recipe => {
                            const item = document.createElement('li');
                            item.className = 'dropdown-item';
                            item.id = `alex-${recipe.id}`;
                            item.textContent = recipe.name;
                            item.addEventListener('click', () => {
                                window.location.href = `http://localhost:5282/recipes/${recipe.id}`;
                            });
                            dropdown.appendChild(item);
                        });
                    }

                    if (!foundRecipes) {
                        const noRecipesItem = document.createElement('li');
                        noRecipesItem.className = 'dropdown-item';
                        noRecipesItem.textContent = 'No recipes found';
                        dropdown.appendChild(noRecipesItem);
                    }
                })
                .catch(error => {
                    console.error('Error fetching Alex\'s recipes:', error);
                });

                dropdown.style.display = 'block';
            } else {
                dropdown.style.display = 'none';
            }
        }, 300)); // Adjust the debounce delay as needed

        // Mark that the event listener has been added
        searchInput.dataset.listenerAdded = true;
    }

    document.addEventListener('click', function(event) {
        if (!searchInput.contains(event.target) && !dropdown.contains(event.target)) {
            dropdown.style.display = 'none';
        }
    });
});