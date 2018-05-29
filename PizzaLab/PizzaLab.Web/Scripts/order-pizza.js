$(function () {
    $("#order-form").submit(function (e) {
        var ingredients = getSelectedIngredients();
        $("#Ingredients").val(ingredients.join(","));
        $(this).submit();
    });

    function getSelectedIngredients() {
        var selectedIngredients = [];

        $(".pizza-ingredient input[type=checkbox]").each(function (i, el) {
            var checkbox = $(el);
            var isIngredientSelected = checkbox.is(":checked");
            if (isIngredientSelected) {
                var ingredientId = checkbox.attr("id").substring("ingredient-".length);
                selectedIngredients.push(Number(ingredientId));
            }
        });

        return selectedIngredients;
    }
});