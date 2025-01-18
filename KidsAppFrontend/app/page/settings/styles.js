import { StyleSheet, Dimensions } from 'react-native';
import color from '@/app/styles/color';

const windowWidth = Dimensions.get('window').width;


const styles = StyleSheet.create({
    container: {
        flex: 1
    },
    buttonContainer: {
        flexDirection: 'row',
        justifyContent: 'center',
    },
    modeContainer: {
        borderBottomWidth: 0.5,
        borderColor: color.GirlColors.Primary,
        marginBottom: 15,
        marginLeft: 30,
        marginRight: 30,
    },
    modeText: {
        marginLeft: 30,
        paddingBottom: 10,
        color: color.GirlColors.Primary,
        fontSize: 20,
        marginRight: 20,
    },
    line: {

    },
    inputContainer: {
        justifyContent: 'center',
        marginTop: 20,
        alignItems: 'center',
        paddingTop: 20,
        marginHorizontal: 20,
        borderRadius: 40,
    },

    buttonContainerTwo: {
        flexDirection: 'row',
        justifyContent: 'center',
        alignItems: 'flex-end',
        marginTop: 170,
        marginBottom: 20
    },

    colors: color,

});

export default styles