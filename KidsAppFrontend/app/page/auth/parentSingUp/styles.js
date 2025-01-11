import {StyleSheet} from 'react-native'

const styles = StyleSheet.create({
    container:{
        flex: 1, 
        justifyContent: 'center', 
        alignItems: 'center' 
    },
    pickerContainer: {
        borderWidth: 1,
        borderColor: '#ccc',
        borderRadius: 4,
        marginVertical: 8,
        overflow: 'hidden',
      },
      picker: {
        height: 50,
        width: '100%',
      },
      errorText: {
        color: 'red',
        fontSize: 12,
        marginBottom: 8,
        marginLeft: 4,
      },
});

export default styles