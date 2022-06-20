import Recipe from '@models/Recipe';
import { Api } from '@utils/api';
import { PageState } from '@utils/constants';
import {
    Alert,
    Skeleton,
    Typography,
} from 'antd';
import useDocumentTitle from '@hooks/useDocumentTitle';
import { useParams } from 'react-router';
import { useEffect, useState } from 'react';

const { Title } = Typography;

const ViewRecipe = (): JSX.Element => {
    const [pageState, setPageState] = useState<PageState>(PageState.Loading);
    const [errorMessage, setErrorMessage] = useState<string>('');
    const [recipe, setRecipe] = useState<Recipe | null>(null);

    const { id } = useParams();

    const fetchRecipe = async () => {
        if (!id) {
            setErrorMessage('Error loading recipe');
            setPageState(PageState.Error);
            return;
        }

        const [data, error] = await Api.Get<Recipe>(`recipes/get?id=${id}`);

        if (error) {
            setErrorMessage(error);
            setPageState(PageState.Error);
            return;
        }

        setRecipe(data);
        setPageState(PageState.Ready);
    };

    useEffect(() => {
        fetchRecipe();
    }, []);

    useDocumentTitle(recipe?.name || '');

    if (pageState === PageState.Loading) {
        return <Skeleton />;
    }

    if (pageState === PageState.Error) {
        return <Alert message={errorMessage} type="error" />;
    }

    if (recipe === null) {
        return <Alert message="Error loading recipe" type="error" />;
    }

    return (
        <Title level={1}>{recipe.name}</Title>
    );
};

export default ViewRecipe;
