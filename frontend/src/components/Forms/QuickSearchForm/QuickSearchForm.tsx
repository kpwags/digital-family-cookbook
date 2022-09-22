import { useState } from 'react';
import { AutoComplete, Input } from 'antd';
import { useNavigate } from 'react-router';
import { Api } from '@utils/api';
import Recipe from '@models/Recipe';

const QuickSearchForm = (): JSX.Element => {
    const [recipes, setRecipes] = useState<{ value: string, label: string }[]>([]);

    const navigate = useNavigate();

    const searchRecipes = async (keywords: string) => {
        const [data, error] = await Api.Get<Recipe[]>('recipes/quicksearch', {
            params: {
                keywords,
                maxRecipes: 5,
            },
        });

        if (error || !data) {
            setRecipes([{ value: '', label: `Search for ${keywords}` }]);
            return;
        }

        setRecipes([
            ...data.map((r) => ({ value: r.recipeId.toString(), label: r.name })),
            { value: '', label: `Search for ${keywords}` },
        ]);
    };

    const onSelect = (recipeId: number, label: string) => {
        if (recipeId > 0) {
            navigate(`/recipes/view/${recipeId}`);
        } else {
            navigate(`/search?q=${label.replace('Search for ', '')}&p=1&rpp=10`);
        }
    };

    return (
        <AutoComplete
            dropdownMatchSelectWidth={252}
            style={{ width: 300 }}
            options={recipes}
            onSelect={(value: number, option: { value: string | number, label: string }) => onSelect(value, option.label)}
            onSearch={(val) => {
                if (val.length >= 3) {
                    searchRecipes(val);
                } else {
                    setRecipes([{ value: '', label: `Search for ${val}` }]);
                }
            }}
        >
            <Input.Search size="large" placeholder="Search Recipes" enterButton />
        </AutoComplete>
    );
};

export default QuickSearchForm;
