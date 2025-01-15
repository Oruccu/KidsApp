import color from '@/app/styles/color';
import {StyleSheet} from 'react-native'

const styles = StyleSheet.create({
    container:{
        flex:1
    },
    buttonContainer:{
        flexDirection:'row',
        justifyContent:'center'
    },
    modeContainer:{
        borderBottomWidth:0.5,
        borderColor:color.GirlColors.Primary,
        marginBottom:15,
        marginLeft:30,
        marginRight:30,
    },
    modeText:{
        marginLeft:30,
        paddingBottom:10,
        color:color.GirlColors.Primary,
        fontSize:20,
        marginRight:20,
    },
    line:{

    }

});

export default styles