document.addEventListener('DOMContentLoaded', function () {
    var swiper = new Swiper('.swiper-container', {
        slidesPerView: 'auto',
        freeMode: true,
        scrollbar: {
            el: '.swiper-scrollbar',
            hide: true,
        },
        breakpoints: {
        }
    });

    const suggestionWrapper = document.getElementById('suggestion-wrapper');
    const fetchedMeals = new Set();
    const maxSuggestions = 4;

    function fetchRandomMeal() {
        return fetch('http://localhost:5085/api/recipe')
            .then(response => response.json())
            .then(data => data[Math.floor(Math.random() * data.length)]);
    }

    async function loadSuggestions() {
        while (fetchedMeals.size < maxSuggestions) {
            const meal = await fetchRandomMeal();
            if (!fetchedMeals.has(meal.id)) {
                fetchedMeals.add(meal.id);

                const slide = document.createElement('div');
                slide.className = 'swiper-slide';
                slide.addEventListener('click', () => {
                    window.location.href = `/recipes/${meal.id}`;
                });

                const img = document.createElement('img');
                img.className = 'custom-img';
                img.src = meal.thumbnail;

                const nameCon = document.createElement('div');
                nameCon.className = 'suggestion-name-con';
                nameCon.textContent = meal.name;

                slide.appendChild(img);
                slide.appendChild(nameCon);
                suggestionWrapper.appendChild(slide);
            }
        }
        swiper.update();
    }

    loadSuggestions();
});
