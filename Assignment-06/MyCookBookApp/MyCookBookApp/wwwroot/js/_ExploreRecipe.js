const exploreRecipeImgCon = document.querySelectorAll('.explore-recipe-img');

exploreRecipeImgCon.forEach(img => {
    img.addEventListener('click', () => {
        const id = img.getAttribute('id');
        window.location.href = `http://localhost:5282/recipes/${id}`;
    });
});
