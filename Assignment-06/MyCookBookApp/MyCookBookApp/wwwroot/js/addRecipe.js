function addIngredient() {
    const container = document.getElementById('ingredients-container');
    const newIngredient = document.createElement('div');
    newIngredient.className = 'ingredients-group';
    newIngredient.innerHTML = `
        <input type="text" name="IngredientNames" placeholder="Ingredient (e.g., Flour)" required>
        <input type="text" name="IngredientQuantities" placeholder="Quantity (e.g., 1 cup)" required>
    `;
    container.appendChild(newIngredient);
}
