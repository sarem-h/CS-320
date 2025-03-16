document.addEventListener('DOMContentLoaded', function () {
    // Add event listener to the "Edit Recipe" button
    const recipeData = JSON.parse(document.getElementById('recipe-data').textContent);
    document.getElementById('edit-recipe-button').addEventListener('click', function () {
        loadEditRecipeForm(recipeData);
    });
});

// Function to display meal details
function displayMealDetails(meal) {
    const container = document.getElementById('recipe-details-container');
    container.innerHTML = `
        <h1 class="text-center my-4">Recipe Details</h1>
        <div class="row">
            <div class="col-md-6">
                <h2 class="meal-title">${meal.name}</h2>
                <img class="meal-img" src="${meal.thumbnail}" alt="${meal.name}" class="img-fluid rounded">
                <div class="mt-2 text-center">
                    <button id="edit-recipe-button" class="btn btn-primary mt-3 shadow-none mr-2" data-toggle="modal" data-target="#editRecipeModal">Edit Recipe</button>
                    <form method="post" action="/Home/DeleteRecipe" style="display:inline;">
                        <input type="hidden" name="id" value="${meal.id}" />
                        <button type="submit" class="btn btn-danger mt-3 shadow-none">Delete Recipe</button>
                    </form>
                </div>
            </div>
            <div class="col-md-6">
                <h4 class="section-title">Instructions</h4>
                <p class="meal-instructions">${meal.instructions}</p>
                <h4 class="section-title">Ingredients</h4>
                <div class="table-responsive" style="max-height: 300px; overflow-y: auto;">
                    <table class="table table-striped">
                        ${getIngredientsTable(meal)}
                    </table>
                </div>
            </div>
        </div>
    `;

    // Add event listener to the "Edit Recipe" button
    document.getElementById('edit-recipe-button').addEventListener('click', function () {
        loadEditRecipeForm(meal);
    });
}

// Function to get ingredients table
function getIngredientsTable(meal) {
    if (!meal.ingredients) {
        return '<tr><td colspan="2">No ingredients available.</td></tr>';
    }

    let ingredients = '';
    for (const [ingredient, measure] of Object.entries(meal.ingredients)) {
        ingredients += `
            <tr>
                <td>${measure}</td>
                <td>${ingredient}</td>
            </tr>
        `;
    }
    return ingredients;
}

// Function to load the edit recipe form into the modal
function loadEditRecipeForm(meal) {
    const formContainer = document.getElementById('edit-recipe-form-container');
    formContainer.innerHTML = `
        <form id="edit-recipe-form" method="post" action="/Home/SaveRecipeChanges">
            <input type="hidden" name="Id" value="${meal.id}">
            <div class="form-group">
                <label for="name">Name</label>
                <input type="text" id="name" name="Name" class="form-control" value="${meal.name}" required>
            </div>
            <div class="form-group">
                <label for="category">Category</label>
                <input type="text" id="category" name="Category" class="form-control" value="${meal.category}" required>
            </div>
            <div class="form-group">
                <label for="instructions">Instructions</label>
                <textarea id="instructions" name="Instructions" class="form-control" rows="4" required>${meal.instructions}</textarea>
            </div>
            <div class="form-group">
                <label for="thumbnail">Thumbnail URL</label>
                <input type="url" id="thumbnail" name="Thumbnail" class="form-control" value="${meal.thumbnail}" required>
            </div>
            <div class="form-group">
                <label for="ingredients">Ingredients</label>
                <div id="ingredients-container">
                    ${getIngredientsInputs(meal.ingredients)}
                </div>
                <div class="d-flex add-ingredient-button" id="add-ingredient-button">
                    <span class="d-flex material-symbols-outlined">
                        add
                    </span>
                    <p>Ingredient</p>
                </div>
            </div>
            <div class="form-group text-center mt-4">
                <button type="submit" class="btn btn-primary">Apply Changes</button>
            </div>
        </form>
    `;

    // Add event listener to the "Add Ingredient" button
    document.getElementById('add-ingredient-button').addEventListener('click', addIngredient);
}

// Function to get ingredients inputs
function getIngredientsInputs(ingredients) {
    let inputs = '';
    for (const [ingredient, measure] of Object.entries(ingredients)) {
        inputs += `
            <div class="ingredients-group">
                <input type="text" name="IngredientNames" class="form-control" value="${ingredient}" required>
                <input type="text" name="IngredientQuantities" class="form-control" value="${measure}" required>
            </div>
        `;
    }
    return inputs;
}

// Function to add a new ingredient input
function addIngredient() {
    const container = document.getElementById('ingredients-container');
    const newIngredient = document.createElement('div');
    newIngredient.className = 'ingredients-group';
    newIngredient.innerHTML = `
        <input type="text" name="IngredientNames" class="form-control" placeholder="Ingredient (e.g., Flour)" required>
        <input type="text" name="IngredientQuantities" class="form-control" placeholder="Quantity (e.g., 1 cup)" required>
    `;
    container.appendChild(newIngredient);
}
